using AngularJS.Models;
using AngularJS.ViewModels;
using AutoMapper;

namespace AngularJS.Utilities.AutoMapperConfigs
{
    public static class GeneralAutoMapperConfigs
    {
        public static MapperConfiguration GetConfigs()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressModel>();
                cfg.CreateMap<Customer, CustomerModel>();
                cfg.CreateMap<CustomerAddress, CustomerAddressModel>();
            });
        }
    }
}