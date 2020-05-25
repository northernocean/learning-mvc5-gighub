using GigHub.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;
using System.Security.Principal;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {

        public GigsControllerTests()
        {
            var identity = new GenericIdentity("user@example.com");
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user@example.com"));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, null);
            var controller = new GigsController();

        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
