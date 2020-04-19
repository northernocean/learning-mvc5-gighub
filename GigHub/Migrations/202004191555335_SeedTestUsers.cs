namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTestUsers : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'fd7e6cb5-a12e-499f-a050-607567f3c8b8', N'artist@gighub.com', 0, N'AIrLqa3Qd2xnLrh0HcxWuUKMrMdXPCo4DJdFEgBmf1tpaHmr3KRfDbtoFm87xPkrAA==', N'd6588071-5a49-44ca-bd57-f0c6ac9a7390', NULL, 0, 0, NULL, 1, 0, N'artist@gighub.com');");

        }

        public override void Down()
        {
        }
    }
}
