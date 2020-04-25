namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedAttendances : DbMigration
    {
        public override void Up()
        {
            //Add user@gighub.com
            Sql("insert into [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', N'user@gighub.com', 0, N'AM84Nfk0YxDAdxK13Adc4WfSEJgsBsQPyDWrJyV5CY0q1UN9nM3X+TuyzHTYnJ82bQ==', N'b1108561-c1af-46f7-8ccd-bb56be2bb007', NULL, 0, 0, NULL, 1, 0, N'user@gighub.com', N'user@gighub.com')");

            //Add two Gigs for artist@gighub.com
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-07-15','Top Hat',2)");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-08-15','Rock Candy Mountain',2)");
            Sql("insert into [dbo].[Gigs] ([ArtistId],[DateTime],[Venue],[GenreId]) values (N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-08-31','Ferry Stage',2)");

            //Set user@gighub.com to attend two gigs
            Sql("insert into [dbo].[Attendances] ([GigId],[AttendeeId]) select top (2) Id, 'a7d12f08-ec6a-435a-b20d-9364e5b26b63' as AttendeeId from [dbo].[Gigs]");
        }

        public override void Down()
        {
        }
    }
}
