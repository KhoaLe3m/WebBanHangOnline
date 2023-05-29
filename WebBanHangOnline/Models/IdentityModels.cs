using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { set; get; }
        public string Phone { set; get; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Adv> Advs { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<Subcribe> Subcribes { set; get; }
        public DbSet<SystemSetting> SystemSettings { set; get; }    
        public DbSet<New> News { set; get; }   
        public DbSet<Statistic> Statistics { set; get; }
        


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        internal IOrderedQueryable<New> Wheres()
        {
            throw new NotImplementedException();
        }
    }
}