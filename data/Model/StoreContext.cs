

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.data.Model.EntityConfigration;
using System.Collections.Generic;

namespace Store.data.Model
{
    // public class StoreContext : IdentityDbContext
    public class StoreContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<PurchaseInvoices> PurchaseInvoices { get; set; }
        public DbSet<PurchInvoiceDetails> PurchInvoiceDetails { get; set; }
        public DbSet<SalesInvoices> SalesInvoices { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Warehouses> Warehouses { get; set; }
        public DbSet<WarhouseStocks> WarhouseStocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=StorDB;Trusted_Connection=True;");
        }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarhouseStocks>().HasKey(c => new { c.ItemId, c.WarehouseId });
            modelBuilder.ApplyConfiguration(configuration: new ProductConfiguration());
            // modelBuilder.ApplyConfiguration(configuration: new UserConfiguration());
            // modelBuilder.Entity<IdentityUser>().HasKey(c => new { c.Id });
             base.OnModelCreating(modelBuilder);
        }
    }

}