namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentDeleted : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotificationDevs", "Comment", c => c.String());
            AlterColumn("dbo.NotificationManagers", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NotificationManagers", "Comment", c => c.Int(nullable: false));
            AlterColumn("dbo.NotificationDevs", "Comment", c => c.Int(nullable: false));
        }
    }
}
