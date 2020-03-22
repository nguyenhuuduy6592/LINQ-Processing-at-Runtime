using AngularJSCore.Models;
using AngularJSCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlLinq;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AngularJSCore.Controllers
{
    public class DefaultController : Controller
    {
        private readonly MyContext context;

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

        public DefaultController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var data = Addresses.AsQueryable()
                 .Where("City == @0", "1-City")
                 .OrderBy("AddressID")
                 .Select("new(City as Name, StateProvince)")
                 .ToDynamicList();

            var output = Addresses.AsQueryable()
                .Join(CustomerAddresses.AsQueryable(),
                    "AddressID", "AddressID", 
                    "new(" +
                        "outer.AddressLine1 as AddressLine1, outer.AddressLine2 as AddressLine2, outer.City as City, outer.CountryRegion as CountryRegion, outer.PostalCode as PostalCode, outer.StateProvince as StateProvince," +
                        "(inner == null ? -1 : inner.CustomerID) as CustomerID, (inner == null ? string.Empty : inner.AddressType) as AddressType" +
                    ")"
                )
                .ToDynamicList();

            var output2 = Addresses.AsQueryable()
                .GroupJoin(CustomerAddresses.AsQueryable(),
                    "AddressID", "AddressID", "new(outer as Addresses, inner as CustomerAddresses)")
                .ToDynamicList()
                .Select(x => new
                {
                    x.Addresses,
                    x.CustomerAddresses
                })
                .ToList();

            var output3 = Addresses.AsQueryable()
                .GroupJoin(CustomerAddresses.AsQueryable(), 
                    "AddressID", "AddressID", "new(outer as Addresses, inner as CustomerAddresses)")
                .SelectMany(
                    "CustomerAddresses.DefaultIfEmpty()",
                    "new(" +
                        "Addresses.AddressLine1 as AddressLine1, outer.AddressLine2 as AddressLine2, outer.City as City, outer.CountryRegion as CountryRegion, outer.PostalCode as PostalCode, outer.StateProvince as StateProvince," +
                        "(inner == null ? -1 : inner.CustomerID) as CustomerID, (inner == null ? string.Empty : inner.AddressType) as AddressType" +
                    ")"
                )
                .ToDynamicList();

            return View(new QueryResult());
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] QueryModel model)
        {
            var result = Addresses.Query<Address, dynamic>(model.Query).ToList();

            return View(new QueryResult
            {
                Query = model.Query,
                Addresses = result
            });
        }
    }
}