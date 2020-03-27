using System;

namespace AngularJS.Models
{
    public class ProductViewModel : BaseEntity
    {
        public int ProductModelID { get; set; }

        public string Name { get; set; }

        public string CatalogDescription { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}