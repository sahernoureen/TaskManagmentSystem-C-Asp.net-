namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BugReportPropertyAddedtoDevtask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DevTasks", "BugReport", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DevTasks", "BugReport");
        }
    }
}
