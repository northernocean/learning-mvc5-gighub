namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTestData : DbMigration
    {

        public override void Up()
        {


            //Add Gigs for artist@gighub.com and pops@gighub.com
            Sql(@"
            SET identity_insert [dbo].[Gigs] on;
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (1,N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-07-15 19:30','Top Hat',2);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (2,N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-08-15 19:30','Rock Candy Mountain',2);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (3,N'fd7e6cb5-a12e-499f-a050-607567f3c8b8','2022-08-31 20:00','Ferry Stage',3);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (4,N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-05 20:00','The Vineyard',4);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (5,N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-06 20:00','Mojo',4);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (6,N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-09 20:00','JJs',4);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (7,N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-13 13:00','Black River Music Festival',4);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (8,N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-14 13:00','Heart Magic',4);
            insert into [dbo].[Gigs] ([Id],[ArtistId],[DateTime],[Venue],[GenreId]) values (9,N'1def91ce-9d0f-401b-ae6e-98b120bf6994','2022-09-18 19:00','Playbook',3);
            set identity_insert [dbo].[Gigs] off");

            //Set user@gighub.com to attend all Lollypops gigs
            Sql("insert into [dbo].[Attendances] ([GigId],[AttendeeId]) select Id, 'a7d12f08-ec6a-435a-b20d-9364e5b26b63' as AttendeeId from [dbo].[Gigs] where ArtistId = N'1def91ce-9d0f-401b-ae6e-98b120bf6994'");

            //Set user@gighub.com to follow Lollypops
            Sql(@"INSERT INTO [dbo].[Followers] ([ArtistId],[UserId]) 
            VALUES (N'1def91ce-9d0f-401b-ae6e-98b120bf6994',N'a7d12f08-ec6a-435a-b20d-9364e5b26b63')");

            //Cancel a gig
            Sql(@"UPDATE [dbo].[Gigs] 
            SET [IsCancelled] = 'True' 
            WHERE [Venue] = 'Black River Music Festival' AND [ArtistId] = N'1def91ce-9d0f-401b-ae6e-98b120bf6994';");

            //Notifications (cancelled, updated - three types, added)
            Sql(@"
            SET IDENTITY_INSERT[dbo].[Notifications] ON;
            INSERT INTO[dbo].[Notifications]
            ([Id], [GigId], [Type], [DateTime], [Venue], [OriginalDateTime], [OriginalVenue], [NotificationDateCreated]) 
            VALUES(1, 7, 1, N'2022-09-13 13:00:00', N'Black River Music Festival', NULL, NULL, N'2020-05-05 20:00:27');
            INSERT INTO [dbo].[Notifications] 
            ([Id], [GigId], [Type], [DateTime], [Venue], [OriginalDateTime], [OriginalVenue], [NotificationDateCreated]) 
            VALUES (2, 5, 2, N'2022-09-06 20:30:00', N'Mojo', N'2022-09-06 20:00:00', N'Mojo', N'2020-05-10 13:53:00');
            INSERT INTO [dbo].[Notifications] 
            ([Id], [GigId], [Type], [DateTime], [Venue], [OriginalDateTime], [OriginalVenue], [NotificationDateCreated]) 
            VALUES (3, 6, 2, N'2022-09-09 20:00:00', N'JJs', N'2022-09-09 20:00:00', N'Fourth Street Stage', N'2020-05-10 13:53:05');
            INSERT INTO [dbo].[Notifications] 
            ([Id], [GigId], [Type], [DateTime], [Venue], [OriginalDateTime], [OriginalVenue], [NotificationDateCreated]) 
            VALUES (4, 8, 2, N'2022-09-14 17:00:00', N'Heart Magic', N'2022-09-14 13:00:00', N'Big Rock Candy Mountain', N'2020-05-10 13:53:36');
            INSERT INTO [dbo].[Notifications] 
            ([Id], [GigId], [Type], [DateTime], [Venue], [OriginalDateTime], [OriginalVenue], [NotificationDateCreated]) 
            VALUES (5, 9, 3, N'2022-09-18 19:00:00', N'Playbook', null, null, N'2020-05-10 13:55:36');
            SET IDENTITY_INSERT[dbo].[Notifications] OFF;");

            Sql(@"
            INSERT INTO [dbo].[UserNotifications] ([NotificationId], [UserId], [IsRead]) 
            VALUES (1, N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', 0)
            INSERT INTO [dbo].[UserNotifications] ([NotificationId], [UserId], [IsRead]) 
            VALUES (2, N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', 0)
            INSERT INTO [dbo].[UserNotifications] ([NotificationId], [UserId], [IsRead]) 
            VALUES (3, N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', 0)
            INSERT INTO [dbo].[UserNotifications] ([NotificationId], [UserId], [IsRead]) 
            VALUES (4, N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', 0)
            INSERT INTO [dbo].[UserNotifications] ([NotificationId], [UserId], [IsRead]) 
            VALUES (5, N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', 0)


            ");
        }

        public override void Down()
        {
            Sql(@"
            delete from [dbo].[Notifications];
            delete from [dbo].[UserNotifications];
            delete from [dbo].[Followers];
            delete from [dbo].[Attendances];
            delete from [dbo].[Gigs];
            delete from [dbo].[AspNetUsers] where [Id] = N'a7d12f08-ec6a-435a-b20d-9364e5b26b63';");
        }

    }
}
