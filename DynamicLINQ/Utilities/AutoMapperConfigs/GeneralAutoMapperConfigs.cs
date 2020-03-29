using DynamicLINQ.Models;
using DynamicLINQ.ViewModels;
using AutoMapper;

namespace DynamicLINQ.Utilities.AutoMapperConfigs
{
    public static class GeneralAutoMapperConfigs
    {
        public static MapperConfiguration GetConfigs()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressViewModel>();
                cfg.CreateMap<CustomerAddress, CustomerAddressViewModel>();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<ProductDescription, ProductDescriptionViewModel>();
                cfg.CreateMap<ProductModelProductDescription, ProductModelProductDescriptionViewModel>();
                cfg.CreateMap<ProductModel, ProductModelViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<SalesOrderDetail, SalesOrderDetailViewModel>();
                cfg.CreateMap<SalesOrderHeader, SalesOrderHeaderViewModel>();
            });
        }
    }
}