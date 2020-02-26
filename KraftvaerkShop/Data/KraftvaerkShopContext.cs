using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KraftvaerkShop.Models;

namespace KraftvaerkShop.Data
{
    public class KraftvaerkShopContext : DbContext
    {
        public KraftvaerkShopContext (DbContextOptions<KraftvaerkShopContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Kana",
                    Price = 3
                },
                new Product
                {
                    Id = 2,
                    Name = "Maito",
                    Price = 1
                }, 
                new Product
                {
                    Id = 3,
                    Name = "Olut",
                    Price = 20
                }, 
                new Product
                {
                    Id = 4,
                    Name = "Peruna",
                    Price = 0.2
                }, 
                new Product
                {
                    Id = 5,
                    Name = "Banaani",
                    Price = 1.5
                }
            );
        }
    }
}
