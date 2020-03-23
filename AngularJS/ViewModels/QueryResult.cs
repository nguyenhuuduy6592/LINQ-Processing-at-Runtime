using AngularJSCore.Models;
using System.Collections.Generic;

namespace AngularJSCore.ViewModels
{
    public class QueryResult : QueryModel
    {
        public QueryResult()
        {
            Addresses = new List<dynamic>();
        }
        public IEnumerable<dynamic> Addresses { get; set; }
    }
}
