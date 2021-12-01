using Microsoft.EntityFrameworkCore;
using ProductService.Domain;
using System.Diagnostics.CodeAnalysis;

namespace ProductService.Infrastructure
{
    public class ProductContext : DbContext
    {
        public ProductContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }


}
