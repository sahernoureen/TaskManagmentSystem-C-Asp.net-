namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationMangaerClassUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationManagers", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.NotificationManagers", "BackgroundColor", c => c.String());
            CreateIndex("dbo.NotificationManagers", "ProjectId");
            AddForeignKey("dbo.NotificationManagers", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationManagers", "ProjectId", "dbo.Projects");
            DropIndex("dbo.NotificationManagers", new[] { "ProjectId" });
            DropColumn("dbo.NotificationManagers", "BackgroundColor");
            DropColumn("dbo.NotificationManagers", "ProjectId");
        }
    }
}
