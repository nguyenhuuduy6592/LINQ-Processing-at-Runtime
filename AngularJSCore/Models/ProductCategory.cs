using System;
using System.ComponentModel.DataAnnotations;

namespace AngularJSCore.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public int ParentProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
