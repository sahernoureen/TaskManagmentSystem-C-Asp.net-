namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentEnumAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotificationDevs", "Comment", c => c.String(nullable: false));
            AlterColumn("dbo.NotificationManagers", "Comment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NotificationManagers", "Comment", c => c.String());
            AlterColumn("dbo.NotificationDevs", "Comment", c => c.String());
        }
    }
}
