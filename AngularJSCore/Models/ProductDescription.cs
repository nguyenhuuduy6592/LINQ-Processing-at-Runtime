using System;

namespace AngularJSCore.Models
{
    public class ProductDescription
    {
        public int ProductDescriptionID { get; set; }
        public string Description { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
