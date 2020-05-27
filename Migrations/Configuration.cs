using TaskManagementSystem.Models;

namespace TaskManagementSystem.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManagementSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManagementSystem.Models.ApplicationDbContext context)
        {
            AdminHelper.addRole("Admin");
            AdminHelper.addRole("Project Manager");
        }
    }
}
