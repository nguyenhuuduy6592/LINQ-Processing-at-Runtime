﻿using DynamicLINQ.Models;
using DynamicLINQ.Services;
using DynamicLINQ.ViewModels;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DynamicLINQ.Helpers
{
    public class ParsingQueryHelpers
    {
        /// <summary>
        /// This method accepts List<T> and userQuery. Data that is filtered is refered as "data" on userQuery.
        /// It compiles the user query as a in memory assembly by filling it in a simple class. Once compiled,
        /// source data is passed in to the assembly instance and excution result is collected.
        /// </summary>
        public static List<dynamic> GetFilteredData<T, V>(List<T> sourceData, List<V> targetData, string userQuery)
        {
            //cast objects to dynamic so that it can be passed on to another assembly.
            var dataSource = sourceData.Cast<dynamic>().ToList();
            var dataTarget = targetData.Cast<dynamic>().ToList();

            #region template Code

            //add required namespaces
            var defaultNamespaces = new[]
                {
                    "System", " System.Dynamic", "System.Collections.Generic", "System.Linq", "System.Text"
                };

            //complete class as string which will be compiled to an in memory assembly
            string executeCode =
                defaultNamespaces.Aggregate("", (current, defaultNamespace) => current + string.Format("using {0};\n", defaultNamespace)) +
                @"namespace MyNamespace {
                    public class MyClass {
                        public List<dynamic> FilterData(List<dynamic> sourceData,  List<dynamic> targetData,  string userQuery) {
                            try{
                                    var result = ((IEnumerable<dynamic>)(" + userQuery + @")).ToList();
                                    return result ;
                               }
                                catch(Exception ex)
                               {
                                    return new List<dynamic> { ""Runtime Exception: "" +  ex.Message + ex.StackTrace };
                               }
                        }

                        private string CheckNull(dynamic data, string property)
                        {
                            return data == null ? string.Empty : data.GetType().GetProperty(property).GetValue(data, null); ;
                        }
                     }    
                }";

            #endregion template Code

            //add required assembly references
            var defaultAssemblies = new[]
                {
                    "System.dll", "System.Core.dll", "Microsoft.CSharp.dll", "System.Data.dll", "System.Xml.dll",
                    "System.Xml.Linq.dll", "System.Windows.Forms.dll"
                };

            var compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
                GenerateExecutable = false,
                CompilerOptions = "/optimize",
            };
            compilerParams.ReferencedAssemblies.AddRange(defaultAssemblies);

            //compile assembly
            var compiledAssembly = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParams, executeCode);

            if (compiledAssembly.Errors.HasErrors)
            {
                var exceptionMessage = compiledAssembly.Errors.Cast<CompilerError>()
                                           .Aggregate("Compilation error on the query:\n",
                                                      (x, y) => x + ("rn" + y.ToString()));

                //MessageBox.Show(exceptionMessage);
                return new List<dynamic>();
            }

            try
            {
                // create instance of the assembly
                dynamic instance =
                    Activator.CreateInstance(compiledAssembly.CompiledAssembly.GetType("MyNamespace.MyClass"));

                //execute the method and collect result. since "instance" is of type dynamic, FiltereData() method will be resolved at run time
                List<dynamic> result = instance.FilterData(dataSource, dataTarget, userQuery);

                if (result.Count() == 1 && result[0].Contains("Runtime Exception: "))
                {
                    //MessageBox.Show(result[0]);
                    return new List<dynamic>();
                }

                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new List<dynamic>();
            }
        }

        /// <summary>
        /// This method accepts List<T> and userQuery. Data that is filtered is refered as "data" on userQuery.
        /// It compiles the user query as a in memory assembly by filling it in a simple class. Once compiled,
        /// source data is passed in to the assembly instance and excution result is collected.
        /// </summary>
        public static IEnumerable<dynamic> GetFilteredData(IEnumerable<dynamic>[] sourceData, List<string> tableName, string userQuery)
        {
            var functionParameters = string.Join(", ", tableName.Select(x => string.Format("IEnumerable<dynamic> {0}", x)));

            #region template Code

            //add required namespaces
            var defaultNamespaces = new[]
                {
                    "System", " System.Dynamic", "System.Collections.Generic", "System.Linq", "System.Text"
                };

            //complete class as string which will be compiled to an in memory assembly
            string executeCode =
                defaultNamespaces.Aggregate("", (current, defaultNamespace) => current + string.Format("using {0};\n", defaultNamespace)) +
                @"namespace MyNamespace {
                    public class MyClass {
                        public IEnumerable<dynamic> FilterData(" + functionParameters + @") {
                            try{
                                    var result = ((IEnumerable<dynamic>)(" + userQuery + @")).ToList();
                                    return result ;
                               }
                                catch(Exception ex)
                               {
                                    return new List<dynamic> { ""Runtime Exception: "" +  ex.Message + ex.StackTrace };
                               }
                        }

                        private object CheckNull(dynamic data, string property)
                        {
                            return data == null ? string.Empty : data.GetType().GetProperty(property).GetValue(data, null); ;
                        }
                     }    
                }";

            #endregion template Code

            //add required assembly references
            var defaultAssemblies = new[]
                {
                    "System.dll", "System.Core.dll", "Microsoft.CSharp.dll", "System.Data.dll", "System.Xml.dll",
                    "System.Xml.Linq.dll", "System.Windows.Forms.dll"
                };

            var compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
                GenerateExecutable = false,
                CompilerOptions = "/optimize",
            };
            compilerParams.ReferencedAssemblies.AddRange(defaultAssemblies);

            //compile assembly
            var compiledAssembly = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParams, executeCode);

            if (compiledAssembly.Errors.HasErrors)
            {
                var exceptionMessage = compiledAssembly.Errors.Cast<CompilerError>()
                                           .Aggregate("Compilation error on the query:\n",
                                                      (x, y) => x + ("rn" + y.ToString()));

                //MessageBox.Show(exceptionMessage);
                return new List<dynamic>();
            }

            try
            {
                var type = compiledAssembly.CompiledAssembly.GetType("MyNamespace.MyClass");
                // create instance of the assembly
                dynamic instance = Activator.CreateInstance(type);

                //execute the method and collect result. since "instance" is of type dynamic, FiltereData() method will be resolved at run time
                MethodInfo magicMethod = type.GetMethod("FilterData");
                dynamic result = magicMethod.Invoke(instance, sourceData);

                if (result.Count == 1 && result[0].Contains("Runtime Exception: "))
                {
                    //MessageBox.Show(result[0]);
                    return new List<dynamic>();
                }

                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new List<dynamic>();
            }
        }
        /// <summary>
        /// This method accepts List<T> and userQuery. Data that is filtered is refered as "data" on userQuery.
        /// It compiles the user query as a in memory assembly by filling it in a simple class. Once compiled,
        /// source data is passed in to the assembly instance and excution result is collected.
        /// </summary>
        public static IEnumerable<dynamic> GetFilteredData(QueryModel model)
        {
            if (string.IsNullOrEmpty(model.Query))
            {
                return new List<dynamic>();
            }

            model.Query = model.Query.Trim();
            var tableList = typeof(MyDbContext).GetProperties().Select(x => x.Name).ToList();
            MatchCollection matches = Regex.Matches(model.Query, "\\s+in\\s+(\\w+)?");
            if (matches.Count == 0)
            {
                return new List<dynamic>();
            }

            var tableListFromUserQuery = matches.Cast<Match>().Select(match => match.Value.Replace(" in ", ""));
            var tableName = (from m in tableListFromUserQuery
                             join t in tableList on m equals t
                             select m)
                            .Distinct()
                            .ToList();
            var functionParameters = string.Join(", ", tableName.Select(x => string.Format("IEnumerable<dynamic> {0}", x)));

            #region template Code

            //add required namespaces
            var defaultNamespaces = new[]
                {
                    "System", " System.Dynamic", "System.Collections.Generic", "System.Linq", "System.Text"
                };

            //complete class as string which will be compiled to an in memory assembly
            if (model.PageSize > 0)
            {
                model.Query = "(" + model.Query + ").Take(" + model.PageSize + ")";
            }
            string executeCode =
                defaultNamespaces.Aggregate("", (current, defaultNamespace) => current + string.Format("using {0};\n", defaultNamespace)) +
                @"namespace MyNamespace {
                    public class MyClass {
                        public IEnumerable<dynamic> FilterData(" + functionParameters + @") {
                            try{
                                    var result = ((IEnumerable<dynamic>)(" + model.Query + @")).ToList();
                                    return result ;
                               }
                                catch(Exception ex)
                               {
                                    return new List<dynamic> { ""Runtime Exception: "" +  ex.Message + ex.StackTrace };
                               }
                        }

                        private object CheckNull(dynamic data, string property)
                        {
                            return data == null ? string.Empty : data.GetType().GetProperty(property).GetValue(data, null); ;
                        }
                     }    
                }";

            #endregion template Code

            //add required assembly references
            var defaultAssemblies = new[]
                {
                    "System.dll", "System.Core.dll", "Microsoft.CSharp.dll", "System.Data.dll", "System.Xml.dll",
                    "System.Xml.Linq.dll", "System.Windows.Forms.dll"
                };

            var compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
                GenerateExecutable = false,
                CompilerOptions = "/optimize",
            };
            compilerParams.ReferencedAssemblies.AddRange(defaultAssemblies);

            //compile assembly
            var compiledAssembly = new CSharpCodeProvider().CompileAssemblyFromSource(compilerParams, executeCode);

            if (compiledAssembly.Errors.HasErrors)
            {
                var exceptionMessage = compiledAssembly.Errors.Cast<CompilerError>()
                                           .Aggregate("Compilation error on the query:\n",
                                                      (x, y) => x + ("rn" + y.ToString()));

                //MessageBox.Show(exceptionMessage);
                return new List<dynamic>();
            }

            try
            {
                var type = compiledAssembly.CompiledAssembly.GetType("MyNamespace.MyClass");
                // create instance of the assembly
                dynamic instance = Activator.CreateInstance(type);

                // get data list
                var sourceData = new object[tableName.Count];
                using (var db = new MyDbContext())
                {
                    for (int i = 0; i < tableName.Count; i++)
                    {
                        switch (tableName[i])
                        {
                            case "Addresses":
                                sourceData[i] = new MongoDbService<AddressViewModel>(tableName[i]).Get();
                                break;
                            case "Customers":
                                sourceData[i] = new MongoDbService<CustomerViewModel>(tableName[i]).Get();
                                break;
                            case "CustomerAddresses":
                                sourceData[i] = new MongoDbService<CustomerAddressViewModel>(tableName[i]).Get();
                                break;
                            case "Products":
                                sourceData[i] = new MongoDbService<ProductViewModel>(tableName[i]).Get();
                                break;
                            case "ProductCategories":
                                sourceData[i] = new MongoDbService<ProductCategoryViewModel>(tableName[i]).Get();
                                break;
                            case "ProductDescriptions":
                                sourceData[i] = new MongoDbService<ProductDescriptionViewModel>(tableName[i]).Get();
                                break;
                            case "ProductModels":
                                sourceData[i] = new MongoDbService<ProductModelViewModel>(tableName[i]).Get();
                                break;
                            case "ProductModelProductDescriptions":
                                sourceData[i] = new MongoDbService<ProductModelProductDescriptionViewModel>(tableName[i]).Get();
                                break;
                            case "SalesOrderDetails":
                                sourceData[i] = new MongoDbService<SalesOrderDetailViewModel>(tableName[i]).Get();
                                break;
                            case "SalesOrderHeaders":
                                sourceData[i] = new MongoDbService<SalesOrderHeaderViewModel>(tableName[i]).Get();
                                break;
                        }
                    }
                }

                //execute the method and collect result. since "instance" is of type dynamic, FiltereData() method will be resolved at run time
                MethodInfo magicMethod = type.GetMethod("FilterData");
                dynamic result = magicMethod.Invoke(instance, sourceData);

                if (result.Count == 1 && result[0].Contains("Runtime Exception: "))
                {
                    //MessageBox.Show(result[0]);
                    return new List<dynamic>();
                }

                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new List<dynamic>();
            }
        }
    }
}
