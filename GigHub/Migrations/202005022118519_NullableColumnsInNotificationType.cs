namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableColumnsInNotificationType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotificationDateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Notifications", "DateTime", c => c.DateTime());
            AlterColumn("dbo.Notifications", "Venue", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "Venue", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Notifications", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notifications", "NotificationDateCreated");
        }
    }
}
