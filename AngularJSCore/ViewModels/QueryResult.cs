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
        public List<dynamic> Addresses { get; set; }
    }
}
