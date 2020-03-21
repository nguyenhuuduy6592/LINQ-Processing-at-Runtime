using Microsoft.EntityFrameworkCore;

namespace AngularJSCore.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SalesLT");

            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductDescription>().ToTable("ProductDescription");
            modelBuilder.Entity<ProductModel>().ToTable("ProductModel");
            modelBuilder.Entity<SalesOrderHeader>().ToTable("SalesOrderHeader");

            modelBuilder.Entity<CustomerAddress>()
                .ToTable("CustomerAddress")
                .HasKey(x => new { x.AddressID, x.CustomerID });

            modelBuilder.Entity<ProductModelProductDescription>()
                .ToTable("ProductModelProductDescription")
                .HasKey(x => new { x.ProductModelID, x.ProductDescriptionID });

            modelBuilder.Entity<SalesOrderDetail>()
                .ToTable("SalesOrderDetail")
                .HasKey(x => new { x.SalesOrderID, x.SalesOrderDetailID });
        }
    }
}
