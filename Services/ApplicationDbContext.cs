using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tienda_Online.Models;

namespace Tienda_Online.Services
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet <ApplicationUser> User { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet <Category> Category { get; set; }
        public DbSet <Location> Location { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet <Order_Details> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(o => o.Product)
                .HasForeignKey(dp => dp.IdProduct)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasMany(p => p.OrderDetails)
                .WithOne(dp => dp.Order)
                .HasForeignKey(dp => dp.IdOrder)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .Ignore(p => p.Location);

            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.IdCategory)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
