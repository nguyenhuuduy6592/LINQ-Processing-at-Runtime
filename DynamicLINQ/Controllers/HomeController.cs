using DynamicLINQ.Helpers;
using DynamicLINQ.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DynamicLINQ.Controllers
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

        public ActionResult Data()
        {
            return View();
        }

        public ActionResult SampleQuery()
        {
            return View();
        }

        public ActionResult TestListLengthLimit()
        {
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
            var list2 = new List<CustomerViewModel>
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
            var list3 = new List<CustomerViewModel>
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
            var list4 = new List<CustomerViewModel>
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
            for (int i = 0; i < 20; i++)
            {
                list.AddRange(list);
                list2.AddRange(list2);
                list3.AddRange(list3);
                list4.AddRange(list4);
            }

            return View();
        }
    }
}