namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserNotificationRefactoring : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Attendances", "Gig_Id", c => c.Int());
            //CreateIndex("dbo.Attendances", "Gig_Id");
            //AddForeignKey("dbo.Attendances", "Gig_Id", "dbo.Gigs", "Id");
        }

        public override void Down()
        {
            //DropForeignKey("dbo.Attendances", "Gig_Id", "dbo.Gigs");
            //DropIndex("dbo.Attendances", new[] { "Gig_Id" });
            //DropColumn("dbo.Attendances", "Gig_Id");
        }
    }
}
