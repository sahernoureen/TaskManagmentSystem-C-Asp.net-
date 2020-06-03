namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsReadpropertyAddedToNotificationDevs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationDevs", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationDevs", "IsRead");
        }
    }
}
