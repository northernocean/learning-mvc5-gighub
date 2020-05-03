namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifications_ROProps : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "GigId", "dbo.Gigs");
            DropIndex("dbo.Notifications", new[] { "GigId" });
            DropColumn("dbo.Notifications", "GigId");
            DropColumn("dbo.Notifications", "Type");
            DropColumn("dbo.Notifications", "DateTime");
            DropColumn("dbo.Notifications", "Venue");
            DropColumn("dbo.Notifications", "OriginalDateTime");
            DropColumn("dbo.Notifications", "OriginalVenue");
            DropColumn("dbo.Notifications", "NotificationDateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "NotificationDateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "OriginalVenue", c => c.String(maxLength: 100));
            AddColumn("dbo.Notifications", "OriginalDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "Venue", c => c.String(maxLength: 100));
            AddColumn("dbo.Notifications", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "GigId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "GigId");
            AddForeignKey("dbo.Notifications", "GigId", "dbo.Gigs", "Id", cascadeDelete: true);
        }
    }
}
