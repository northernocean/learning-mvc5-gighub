namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.NotificationId, t.UserId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GigId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                        Venue = c.String(maxLength: 100),
                        OriginalDateTime = c.DateTime(),
                        OriginalVenue = c.String(maxLength: 100),
                        NotificationDateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gigs", t => t.GigId, cascadeDelete: true)
                .Index(t => t.GigId);
            
            AddColumn("dbo.Gigs", "IsCancelled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "GigId", "dbo.Gigs");
            DropIndex("dbo.Notifications", new[] { "GigId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            AlterColumn("dbo.Gigs", "Venue", c => c.String(nullable: false));
            DropColumn("dbo.Gigs", "IsCancelled");
            DropTable("dbo.Notifications");
            DropTable("dbo.UserNotifications");
        }
    }
}
