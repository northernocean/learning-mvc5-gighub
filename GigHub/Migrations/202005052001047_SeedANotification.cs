namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedANotification : DbMigration
    {
        public override void Up()
        {
            Sql(@"UPDATE [dbo].[Gigs] 
            SET [IsCancelled] = 'True' 
            WHERE [Venue] = 'Black River Music Festival' AND [ArtistId] = N'1def91ce-9d0f-401b-ae6e-98b120bf6994';");

            Sql(@"
            DELETE FROM dbo.Notifications where Id = 1;
            SET IDENTITY_INSERT[dbo].[Notifications] ON;
            INSERT INTO[dbo].[Notifications]
            ([Id], [GigId], [Type], [DateTime], [Venue], [OriginalDateTime], [OriginalVenue], [NotificationDateCreated]) 
            VALUES(1, 5, 1, N'2022-09-13 13:00:00', N'Black River Music Festival', NULL, NULL, N'2020-05-05 20:00:27');
            SET IDENTITY_INSERT[dbo].[Notifications] OFF;");

            Sql(@"
            INSERT INTO [dbo].[UserNotifications] ([NotificationId], [UserId], [IsRead]) 
            VALUES (1, N'a7d12f08-ec6a-435a-b20d-9364e5b26b63', 0)");
        }

        public override void Down()
        {
        }
    }
}
