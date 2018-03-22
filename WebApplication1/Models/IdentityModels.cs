using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Domain.Core;

namespace WebApplication1.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } // добавляем свойство Name
        public string Surname { get; set; } // добавляем свойство Surname

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<Nakonechnik> Nakonechniks { get; set; }
        //public DbSet<Lin> Lins { get; set; }
        //public DbSet<Garpun> Garpuns { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Arbalet> Arbalets { get; set; }
        //public DbSet<Pneumatic> Pneumatics { get; set; }
        //public DbSet<Brand> Brands { get; set; }
        public ApplicationDbContext()
            : base("OrderContext", throwIfV1Schema: false)
        {
          
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    
}