using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniWebShop.Models.Dbo;
using MiniWebShop.Models.Dbo.OrderModels;
using MiniWebShop.Models.Dbo.ProductModels;
using MiniWebShop.Models.Dbo.UserModel;

namespace MiniWebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Address>().HasData(new Address
            {
                Id = 1,
                Country = "Croatia",
                City = "Zagreb",
                Street = "Kunovecka",
                Number = "10b"

            });
         
            base.OnModelCreating(modelbuilder);
        }

        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SessionItem> SessionItems { get; set; }
        public DbSet<Address> Addresss { get; set; }
    }
}
