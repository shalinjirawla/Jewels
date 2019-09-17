using Inventory.Core.Models;
using Inventory.Core.Models.ApplicationUser;
using Inventory.Core.Models.Commons;
using Inventory.Core.Models.Currency;
using Inventory.Core.Models.Customer;
using Inventory.Core.Models.Products;
using Inventory.Core.Models.Supplier;
using Inventory.Core.Models.Tenants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.EntityFrameworkCore.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Tenants> Tenants { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Adderss> Addersses { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Country> country { get; set; }
        public DbSet<DiscountType> discountTypes { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<CreditTerms> CreditTerms { get; set; }
        public DbSet<TaxCode> TaxCode { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ShipmentTerm> ShipmentTerms { get; set; }
        public DbSet<ShipmentMethod> ShipmentMethods { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<RawMaterails> RawMaterails { get; set; }
        public DbSet<UOM> UOMs { get; set; }
    }
}
