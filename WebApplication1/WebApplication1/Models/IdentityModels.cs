using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Projects = new HashSet<Project>();
            this.Reports = new HashSet<Report>();
        }
        
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

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
        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = true;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApplication1.Models.Project_User> Project_User { get; set; }

        //public System.Data.Entity.DbSet<WebApplication1.Models.BacklogTask> BacklogTasks { get; set; }

        //public System.Data.Entity.DbSet<WebApplication1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}