using Microservices.Services.ProductAPI.Models;
using Microservices.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }

    }
}