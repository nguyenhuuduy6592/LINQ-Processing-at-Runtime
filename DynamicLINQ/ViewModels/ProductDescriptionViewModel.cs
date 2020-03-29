using DynamicLINQ.Models;
using System;

namespace DynamicLINQ.ViewModels
{
     public class ProductDescriptionViewModel : BaseEntity
    {
        public int ProductDescriptionID { get; set; }

        public string Description { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
