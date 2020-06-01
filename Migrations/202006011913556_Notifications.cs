namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationDevs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        DeveloperId = c.String(maxLength: 128),
                        Comment = c.String(),
                        TaskId = c.Int(nullable: false),
                        DevTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeveloperId)
                .ForeignKey("dbo.DevTasks", t => t.DevTask_Id)
                .Index(t => t.DeveloperId)
                .Index(t => t.DevTask_Id);
            
            CreateTable(
                "dbo.NotificationManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        ProjectManagerId = c.String(maxLength: 128),
                        Comment = c.String(),
                        TaskId = c.Int(nullable: false),
                        DevTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DevTasks", t => t.DevTask_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProjectManagerId)
                .Index(t => t.ProjectManagerId)
                .Index(t => t.DevTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationManagers", "ProjectManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationManagers", "DevTask_Id", "dbo.DevTasks");
            DropForeignKey("dbo.NotificationDevs", "DevTask_Id", "dbo.DevTasks");
            DropForeignKey("dbo.NotificationDevs", "DeveloperId", "dbo.AspNetUsers");
            DropIndex("dbo.NotificationManagers", new[] { "DevTask_Id" });
            DropIndex("dbo.NotificationManagers", new[] { "ProjectManagerId" });
            DropIndex("dbo.NotificationDevs", new[] { "DevTask_Id" });
            DropIndex("dbo.NotificationDevs", new[] { "DeveloperId" });
            DropTable("dbo.NotificationManagers");
            DropTable("dbo.NotificationDevs");
        }
    }
}
