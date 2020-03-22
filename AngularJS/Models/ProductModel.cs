using System;
using System.ComponentModel.DataAnnotations;

namespace AngularJSCore.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductModelID { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
