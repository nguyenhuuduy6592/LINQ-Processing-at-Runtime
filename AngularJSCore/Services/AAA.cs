using System;
using System.Collections.Generic;
using System.Linq;
namespace MyNamespace
{
    public class MyClass
    {
        public List<dynamic> FilterData(List<dynamic> sourceData, List<dynamic> targetData, string userQuery)
        {
            try
            {
                var result = ((IEnumerable<dynamic>)(
                    from a in sourceData
                    join c in targetData on a.AddressID equals c.AddressID
                    select new
                    {
                        a.AddressLine1,
                        c.CustomerID,
                        c.AddressType
                    }
                )).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<dynamic> { ex.Message + ex.StackTrace };
            }
        }

        private string CheckNull(dynamic data, string property)
        {
            return data == null ? string.Empty : data.GetType().GetProperty(property).GetValue(data, null); ;
        }
    }
}