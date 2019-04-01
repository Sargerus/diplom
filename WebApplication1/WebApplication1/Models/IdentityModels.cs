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
            this.Backlogs = new HashSet<Backlog>();
            this.BacklogTasks = new HashSet<BacklogTask>();
            this.Reports = new HashSet<Report>();
        }

        public bool isManager { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Backlog> Backlogs { get; set; }
        public virtual ICollection<BacklogTask> BacklogTasks { get; set; }
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
        public DbSet<Backlog> Backlogs { get; set; }
        public DbSet<BacklogState> BacklogStates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BacklogTask> BacklogTasks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<BacklogType> BacklogTypes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = true;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<WebApplication1.Models.BacklogTask> BacklogTasks { get; set; }

        //public System.Data.Entity.DbSet<WebApplication1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}