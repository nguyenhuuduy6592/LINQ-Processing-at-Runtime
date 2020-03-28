using AngularJS.Models;
using AngularJS.Services;
using AngularJS.Utilities.AutoMapperConfigs;
using AngularJS.ViewModels;
using AngularJSCore.Helpers;
using AngularJSCore.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AngularJS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new QueryResult());
        }

        [HttpPost]
        public async Task<ActionResult> Index(QueryModel model)
        {
            var result = new QueryResult
            {
                Query = model.Query,
            };

            if (string.IsNullOrEmpty(model.Query))
            {
                ViewBag.ErrorMessage = "Please enter query.";
                return View(result);
            }

            var data = ParsingQueryHelpers.GetFilteredData(model);

            ViewBag.ErrorMessage = "";
            result.Addresses = data;
            return View(result);
        }

        public ActionResult GenerateCollection()
        {
            var config = GeneralAutoMapperConfigs.GetConfigs();
            var mapper = new Mapper(config);
            var validationMongoDbService = new MongoDbService<AddressViewModel>();

            using (var db = new MyDbContext())
            {
                if (validationMongoDbService.IsCollectionExist("Addresses"))
                {
                    new MongoDbService<AddressViewModel>("Addresses")
                        .InsertMany(mapper.Map<List<AddressViewModel>>(db.Addresses.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("Customers"))
                {
                    new MongoDbService<CustomerViewModel>("Customers")
                        .InsertMany(mapper.Map<List<CustomerViewModel>>(db.Customers.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("CustomerAddresses"))
                {
                    new MongoDbService<CustomerAddressViewModel>("CustomerAddresses")
                        .InsertMany(mapper.Map<List<CustomerAddressViewModel>>(db.CustomerAddresses.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("Products"))
                {
                    new MongoDbService<ProductViewModel>("Products")
                        .InsertMany(mapper.Map<List<ProductViewModel>>(db.Products.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("ProductCategories"))
                {
                    new MongoDbService<ProductCategoryViewModel>("ProductCategories")
                        .InsertMany(mapper.Map<List<ProductCategoryViewModel>>(db.ProductCategories.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("ProductDescriptions"))
                {
                    new MongoDbService<ProductDescriptionViewModel>("ProductDescriptions")
                        .InsertMany(mapper.Map<List<ProductDescriptionViewModel>>(db.ProductDescriptions.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("ProductModels"))
                {
                    new MongoDbService<ProductModelViewModel>("ProductModels")
                        .InsertMany(mapper.Map<List<ProductModelViewModel>>(db.ProductModels.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("ProductModelProductDescriptions"))
                {
                    new MongoDbService<ProductModelProductDescriptionViewModel>("ProductModelProductDescriptions")
                        .InsertMany(mapper.Map<List<ProductModelProductDescriptionViewModel>>(db.ProductModelProductDescriptions.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("SalesOrderDetails"))
                {
                    new MongoDbService<SalesOrderDetailViewModel>("SalesOrderDetails")
                        .InsertMany(mapper.Map<List<SalesOrderDetailViewModel>>(db.SalesOrderDetails.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("SalesOrderHeaders"))
                {
                    new MongoDbService<SalesOrderHeaderViewModel>("SalesOrderHeaders")
                        .InsertMany(mapper.Map<List<SalesOrderHeaderViewModel>>(db.SalesOrderHeaders.ToList()));
                }
            }

            return Content("Ok");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var list = new List<CustomerViewModel>
            {
                new CustomerViewModel
                {
                    CompanyName = "Nguyen Huu Duy",
                    CustomerID = 1,
                    EmailAddress = "nguyenhuuduy@gmail",
                    FirstName = "Nguyen",
                    MiddleName = "Huu",
                    LastName = "Duy",
                    Id = "djhkdf87223h0dfnsdf9sdfn",
                    ModifiedDate = DateTime.Now,
                    NameStyle = true,
                    PasswordHash = "asdasd",
                    PasswordSalt = "1223",
                    Phone = "0123456",
                    rowguid = Guid.NewGuid(),
                    SalesPerson = "DUy",
                    Suffix = "Mr",
                    Title = "Nothing"
                }
            };
            for (int i = 0; i < 50; i++)
            {
                list.AddRange(list);
            }
            int identificador = GC.GetGeneration(list);
            list.Clear();
            GC.Collect(identificador, GCCollectionMode.Forced);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}