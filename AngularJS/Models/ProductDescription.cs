using System;
using System.ComponentModel.DataAnnotations;

namespace AngularJSCore.Models
{
    public class ProductDescription
    {
        [Key]
        public int ProductDescriptionID { get; set; }
        public string Description { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
