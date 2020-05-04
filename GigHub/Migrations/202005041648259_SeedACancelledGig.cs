namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedACancelledGig : DbMigration
    {

        public override void Up()
        {
            Sql("update [dbo].[Gigs] set IsCancelled = 'True' where Venue = 'Market Street Cafe (Cancelled)';");
        }

        public override void Down()
        {
            Sql("update [dbo].[Gigs] set IsCancelled = 'False' where Venue = 'Market Street Cafe (Cancelled)';");
        }
    
    }
}
