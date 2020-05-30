using System;
using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            int result = 1;
            Assert.AreEqual(result, 1);
        }
    }
}
