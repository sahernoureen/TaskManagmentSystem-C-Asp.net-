namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataAnnotationInDevTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DevTasks", "Deadline", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.DevTasks", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.DevTasks", "DateCompleted", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DevTasks", "DateCompleted", c => c.DateTime());
            AlterColumn("dbo.DevTasks", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DevTasks", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
