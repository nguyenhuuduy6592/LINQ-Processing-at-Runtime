using System;

namespace AngularJSCore.Models
{
    public class SalesOrderDetail
    {
        public int SalesOrderID { get; set; }
        public int SalesOrderDetailID { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public double UnitPrice { get; set; }
        public double UnitPriceDiscount { get; set; }
        public double LineTotal { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
