namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedGenres : DbMigration
    {
        public override void Up()
        {

            Sql("insert into dbo.Genres (Id, [Name]) values (1, 'Jazz');");
            Sql("insert into dbo.Genres (Id, [Name]) values (2, 'Blues');");
            Sql("insert into dbo.Genres (Id, [Name]) values (3, 'Rock');");
            Sql("insert into dbo.Genres (Id, [Name]) values (4, 'Country');");

        }

        public override void Down()
        {
            Sql("delete from dbo.Genres where Id in (1,2,3,4);");
        }
    }
}
