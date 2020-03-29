using DynamicLINQ.Models;
using System;

namespace DynamicLINQ.ViewModels
{
    public class ProductModelProductDescriptionViewModel : BaseEntity
    {
        public int ProductModelID { get; set; }

        public int ProductDescriptionID { get; set; }

        public string Culture { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
