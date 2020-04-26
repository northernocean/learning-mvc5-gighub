namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedTestUsers : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES(N'fd7e6cb5-a12e-499f-a050-607567f3c8b8', N'artist@gighub.com', 0, N'AIrLqa3Qd2xnLrh0HcxWuUKMrMdXPCo4DJdFEgBmf1tpaHmr3KRfDbtoFm87xPkrAA==', N'd6588071-5a49-44ca-bd57-f0c6ac9a7390', NULL, 0, 0, NULL, 1, 0, N'artist@gighub.com', N'Madcat & Kane');");
            Sql("INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES(N'1def91ce-9d0f-401b-ae6e-98b120bf6994', N'pops@gighub.com', 0, N'APYkErMU5l93luQc/JtxbJ1GVK6yRE2WRFPSOlVMM3mmIf8sQpkjEFgRh2v6f142zA==', N'64a72a46-183a-40cb-bfa1-c72be331a113', NULL, 0, 0, NULL, 1, 0, N'pops@gighub.com', N'Lollypops');");

        }

        public override void Down()
        {
        }
    }
}