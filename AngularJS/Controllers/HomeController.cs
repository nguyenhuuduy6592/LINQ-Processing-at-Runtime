using AngularJS.Models;
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

            var tableName = new List<string> { "Addresses", "CustomerAddresses", "Customers" };
            var data = ParsingQueryHelpers.GetFilteredData(tableName, model.Query);

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
    }
}