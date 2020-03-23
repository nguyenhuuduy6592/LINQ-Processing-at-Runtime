using AngularJSCore.Helpers;
using AngularJSCore.Models;
using AngularJSCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AngularJS.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<dynamic> _addresses;
        private readonly List<dynamic> _customerAddress;

        public HomeController()
        {
            _addresses = Addresses.Cast<dynamic>().ToList();
            _customerAddress = CustomerAddresses.Cast<dynamic>().ToList();
        }

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

            var input = new List<dynamic>[] { _addresses, _customerAddress };
            var tableName = new List<string> { "Addresses", "CustomerAddresses" };
            var data = ParsingQueryHelpers.GetFilteredData(input, tableName, model.Query);

            ViewBag.ErrorMessage = "";
            result.Addresses = data;
            return View(result);
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
        private List<Address> Addresses = new List<Address>
        {
            new Address
            {
                AddressID = 1,
                AddressLine1 = "1-AddressLine1",
                AddressLine2 = "1-AddressLine2",
                City = "1-City",
                CountryRegion = "1-CountryRegion",
                PostalCode = "1-PostalCode",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),
                StateProvince = "1-StateProvince"
            },
            new Address
            {
                AddressID = 2,
                AddressLine1 = "2-AddressLine1",
                AddressLine2 = "2-AddressLine2",
                City = "2-City",
                CountryRegion = "2-CountryRegion",
                PostalCode = "2-PostalCode",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),
                StateProvince = "2-StateProvince"
            }
        };

        private List<CustomerAddress> CustomerAddresses = new List<CustomerAddress>
        {
            new CustomerAddress
            {
                AddressID = 1,
                CustomerID = 1,
                AddressType = "Home",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            },
            new CustomerAddress
            {
                AddressID = 1,
                CustomerID = 2,
                AddressType = "Work",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            }
        };

    }
}