using GigHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {

        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDBToLatestVersion();
        }

        public void Seed()
        {
            //Not used because our migration has a seed function for initial test data already
            var context = new ApplicationDbContext();
        }

        void MigrateDBToLatestVersion()
        {
            var configuration = new Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

    }
}
