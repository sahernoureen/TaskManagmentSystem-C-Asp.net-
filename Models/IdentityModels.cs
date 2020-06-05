using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.Models.ProjectClasses;

namespace TaskManagementSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Projects = new HashSet<Project>();
            Tasks = new HashSet<DevTask>();
            NotificationDev = new HashSet<NotificationDev>();
            NotificationManager = new HashSet<NotificationManager>();
        }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<DevTask> Tasks { get; set; }
        public virtual ICollection<NotificationDev> NotificationDev { get; set; }
        public virtual ICollection<NotificationManager> NotificationManager { get; set; }

        public double Salary { set; get; }
        public string TeamName { get; set; }


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
        public DbSet<DevTask> DevTasks { get; set; }
        public DbSet<NotificationDev> NotificationDevs { get; set; }
        public DbSet<NotificationManager> NotificationManagers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().Property(x => x.DateCompleted).IsOptional();

            modelBuilder.Entity<DevTask>().Property(x => x.DateCompleted).IsOptional();
            modelBuilder.Entity<DevTask>().Property(x => x.DeveloperId).IsRequired();
            modelBuilder.Entity<DevTask>().Property(x => x.PercentCompleted).IsOptional();

        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TaskManagementSystem.Models.Helper.UserInfoHolder> UserInfoHolders { get; set; }
    }
}