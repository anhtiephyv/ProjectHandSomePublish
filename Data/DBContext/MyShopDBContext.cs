using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Data.DBContext
{
    // Context của tôi kế thừa từ thằng ApplicationUser để sau này sẽ tự động create các bảng liên quan đến user
    public class MyShopDBContext : IdentityDbContext<ApplicationUser>
    {
        public MyShopDBContext()
            : base("FashionShop")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Country> Country { get; set; }
        public DbSet<UserCountry> UserCountry { get; set; }
        public DbSet<Users> UsersUndefined { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        //   public DbSet<Color> Color { get; set; }

        //  public DbSet<ApplicationUser> ApplicationIdentity  { get; set;}
        public static MyShopDBContext Create()
        {
            return new MyShopDBContext();
        }
        // 1 cú tạo relationship
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
            builder.Entity<Category>()
                           .HasMany(e => e.CategoryChildren)
                           .WithOptional(e => e.CategoryParent)
                           .HasForeignKey(e => e.ParentCategoryID);
            builder.Entity<Product>()
                            .HasOptional(e => e.ProductCategory)
                            .WithMany(e => e.Products)
                            .HasForeignKey(e => e.CategoryID);

        }
    }

}