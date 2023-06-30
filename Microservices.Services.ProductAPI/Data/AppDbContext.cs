using Microservices.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Product 1", Price = 100, Dsecprition = "Description 1", CategoryName = "Category 1", ImageUrl = "https://picsum.photos/200/300?random=1" },
                new Product { ProductId = 2, Name = "Product 2", Price = 200, Dsecprition = "Description 2", CategoryName = "Category 2", ImageUrl = "https://picsum.photos/200/300?random=2" },
                new Product { ProductId = 3, Name = "Product 3", Price = 300, Dsecprition = "Description 3", CategoryName = "Category 3", ImageUrl = "https://picsum.photos/200/300?random=3" },
                new Product { ProductId = 4, Name = "Product 4", Price = 400, Dsecprition = "Description 4", CategoryName = "Category 4", ImageUrl = "https://picsum.photos/200/300?random=4" },
                new Product { ProductId = 5, Name = "Product 5", Price = 500, Dsecprition = "Description 5", CategoryName = "Category 5", ImageUrl = "https://picsum.photos/200/300?random=5" },
                new Product { ProductId = 6, Name = "Product 6", Price = 600, Dsecprition = "Description 6", CategoryName = "Category 6", ImageUrl = "https://picsum.photos/200/300?random=6" },
                new Product { ProductId = 7, Name = "Product 7", Price = 700, Dsecprition = "Description 7", CategoryName = "Category 7", ImageUrl = "https://picsum.photos/200/300?random=7" },
                new Product { ProductId = 8, Name = "Product 8", Price = 800, Dsecprition = "Description 8", CategoryName = "Category 8", ImageUrl = "https://picsum.photos/200/300?random=8" },
                new Product { ProductId = 9, Name = "Product 9", Price = 900, Dsecprition = "Description 9", CategoryName = "Category 9", ImageUrl = "https://picsum.photos/200/300?random=9" },
                new Product { ProductId = 10, Name = "Product 10", Price = 1000, Dsecprition = "Description 10", CategoryName = "Category 10", ImageUrl = "https://picsum.photos/200/300?random=10" }
            );
        }

    }
}
