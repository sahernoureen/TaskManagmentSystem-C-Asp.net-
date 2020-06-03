namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backgroundcolorPropertydeletedfromManagerNoti : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.NotificationManagers", "BackgroundColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotificationManagers", "BackgroundColor", c => c.String());
        }
    }
}
