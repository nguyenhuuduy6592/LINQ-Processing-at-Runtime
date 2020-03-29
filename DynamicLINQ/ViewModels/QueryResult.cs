using System.Collections.Generic;

namespace DynamicLINQ.ViewModels
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
