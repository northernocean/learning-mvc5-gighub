namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedAttendances : DbMigration
    {
        public override void Up()
        {

            //Add Gigs for artist@gighub.com and pops@gighub.com
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-07-15 19:30','Top Hat',2);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-08-15 19:30','Rock Candy Mountain',2);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-08-31 20:00','Ferry Stage',3);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-05 20:00','The Vineyard',4);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-06 20:00','Mojo',4);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-09 20:00','JJs',4);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-13 13:00','Black River Music Festival',4);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-14 13:00','Heart Magic',4);");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-18 19:00','Playbook',3);");

            //Set user@gighub.com to attend all Lollypops Pops gigs
            Sql("insert into [dbo].[Attendances] ([GigId],[AttendeeId]) select Id, 'a7d12f08-ec6a-435a-b20d-9364e5b26b63' as AttendeeId from [dbo].[Gigs] where ArtistId = N'1def91ce-9d0f-401b-ae6e-98b120bf6994'");
        }

        public override void Down()
        {
            Sql("delete from [dbo].[Attendances]");
            Sql("delete from [dbo].[Gigs]");
            Sql("delete from [dbo].[AspNetUsers] where [Id] = N'a7d12f08-ec6a-435a-b20d-9364e5b26b63'");
        }
    }
}
