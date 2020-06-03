namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNameChangedInNotificationManager : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationManagers", "IsChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationManagers", "IsChecked");
        }
    }
}
