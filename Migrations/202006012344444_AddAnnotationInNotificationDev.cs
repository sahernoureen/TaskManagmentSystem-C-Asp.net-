namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationInNotificationDev : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationDevs", "DevTask_Id", "dbo.DevTasks");
            DropIndex("dbo.NotificationDevs", new[] { "DevTask_Id" });
            AddColumn("dbo.DevTasks", "NotificationDevs_Id", c => c.Int());
            AddColumn("dbo.NotificationDevs", "DevTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.DevTasks", "NotificationDevs_Id");
            AddForeignKey("dbo.DevTasks", "NotificationDevs_Id", "dbo.NotificationDevs", "Id");
            DropColumn("dbo.NotificationDevs", "TaskId");
            DropColumn("dbo.NotificationDevs", "DevTask_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotificationDevs", "DevTask_Id", c => c.Int());
            AddColumn("dbo.NotificationDevs", "TaskId", c => c.Int(nullable: false));
            DropForeignKey("dbo.DevTasks", "NotificationDevs_Id", "dbo.NotificationDevs");
            DropIndex("dbo.DevTasks", new[] { "NotificationDevs_Id" });
            DropColumn("dbo.NotificationDevs", "DevTaskId");
            DropColumn("dbo.DevTasks", "NotificationDevs_Id");
            CreateIndex("dbo.NotificationDevs", "DevTask_Id");
            AddForeignKey("dbo.NotificationDevs", "DevTask_Id", "dbo.DevTasks", "Id");
        }
    }
}
