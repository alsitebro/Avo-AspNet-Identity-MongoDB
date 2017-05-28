using NUnit.Framework;
using System.Linq;
using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
    public class UserSecurityStampStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public void Create_NewUser_HasSecurityStamp()
        {
            var manager = GetUserManager();
            var user = new IdentityUser("bob");

            manager.Create(user);

            var savedUser = Users.AsQueryable().Single();
            Expect(savedUser.SecurityStamp, Is.Not.Null);
        }

        [Test]
        public void GetSecurityStamp_NewUser_ReturnsStamp()
        {
            var manager = GetUserManager();
            var user = new IdentityUser("bob");
            manager.Create(user);

            var stamp = manager.GetSecurityStamp(user.Id);

            Expect(stamp, Is.Not.Null);
        }
    }
}
