﻿using DynamicLINQ.Models;
using System;

namespace DynamicLINQ.ViewModels
{
    public class CustomerAddressViewModel : BaseEntity
    {
        public int CustomerID { get; set; }

        public int AddressID { get; set; }

        public string AddressType { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}