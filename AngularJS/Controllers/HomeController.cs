using AngularJS.Models;
using AngularJS.Services;
using AngularJS.Utilities.AutoMapperConfigs;
using AngularJS.ViewModels;
using AngularJSCore.Helpers;
using AngularJSCore.ViewModels;
using AutoMapper;
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
            var validationMongoDbService = new MongoDbService<AddressModel>();

            using (var db = new MyDbContext())
            {
                if (validationMongoDbService.IsCollectionExist("Addresses"))
                {
                    new MongoDbService<AddressModel>("Addresses")
                        .InsertMany(mapper.Map<List<AddressModel>>(db.Addresses.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("Customers"))
                {
                    new MongoDbService<CustomerModel>("Customers")
                        .InsertMany(mapper.Map<List<CustomerModel>>(db.Customers.ToList()));
                }

                if (validationMongoDbService.IsCollectionExist("CustomerAddresses"))
                {
                    new MongoDbService<CustomerAddressModel>("CustomerAddresses")
                        .InsertMany(mapper.Map<List<CustomerAddressModel>>(db.CustomerAddresses.ToList()));
                }
            }

            return Content("Ok");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}