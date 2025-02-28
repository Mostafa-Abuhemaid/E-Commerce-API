using E_Commerce.Core.Entities;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>

    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Special_Offers> Special_Offers { get; set;}
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
               .HasKey(ci => new { ci.CartId, ci.ProductId });

            modelBuilder
        .Entity<Order>()
        .Property(o => o.status)
        .HasConversion(new EnumToStringConverter<OrderStatus>());
        }
    }
}
