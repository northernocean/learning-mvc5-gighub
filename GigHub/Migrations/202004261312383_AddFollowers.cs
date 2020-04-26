namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFollowers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followers",
                c => new
                {
                    ArtistId = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.ArtistId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Followers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followers", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Followers", new[] { "UserId" });
            DropTable("dbo.Followers");
        }
    }
}
