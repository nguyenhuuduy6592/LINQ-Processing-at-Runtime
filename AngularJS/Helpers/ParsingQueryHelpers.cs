using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace AngularJSCore.Helpers
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
    }
}
