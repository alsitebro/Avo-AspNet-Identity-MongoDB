using System.Linq;
using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
    public class UserStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public void Create_NewUser_Saves()
        {
            var userName = "name";
            var user = new IdentityUser(userName);
            var manager = GetUserManager();

            manager.Create(user);

            var savedUser = Users.AsQueryable().Single();
            Assert.That(savedUser.UserName, Is.EqualTo(user.UserName));
        }

        [Test]
        public void FindByName_SavedUser_ReturnsUser()
        {
            var userName = "name";
            var user = new IdentityUser(userName);
            var manager = GetUserManager();
            manager.Create(user);

            var foundUser = manager.FindByName(userName);

            Assert.That(foundUser, Is.Not.Null);
            Assert.That(foundUser.UserName, Is.EqualTo(userName));
        }

        [Test]
        public void FindByName_NoUser_ReturnsNull()
        {
            var manager = GetUserManager();

            var foundUser = manager.FindByName("nouserbyname");

            Assert.That(foundUser, Is.Null);
        }

        [Test]
        public void FindById_SavedUser_ReturnsUser()
        {
            var user = new IdentityUser("name");
            var manager = GetUserManager();
            manager.Create(user);

            var foundUser = manager.FindById(user.Id);

            Assert.That(foundUser, Is.Not.Null);
            Assert.That(foundUser.Id, Is.EqualTo(user.Id));
        }

        [Test]
        public void FindById_NoUser_ReturnsNull()
        {
            var manager = GetUserManager();

            var foundUser = manager.FindById(ObjectId.GenerateNewId().ToString());

            Assert.That(foundUser, Is.Null);
        }

        [Test]
        public void Delete_ExistingUser_Removes()
        {
            var user = new IdentityUser("name");
            var manager = GetUserManager();
            manager.Create(user);
            Assert.That(Users.AsQueryable(), Is.Not.Empty);

            manager.Delete(user);

            Assert.That(Users.AsQueryable(), Is.Empty);
        }

        [Test]
        public void Update_ExistingUser_Updates()
        {
            var user = new IdentityUser("name");
            var manager = GetUserManager();
            manager.Create(user);
            var savedUser = manager.FindById(user.Id);
            savedUser.UserName = "newname";

            manager.Update(savedUser);

            var changedUser = Users.AsQueryable().Single();
            Assert.That(changedUser, Is.Not.Null);
            Assert.That(changedUser.UserName, Is.EqualTo("newname"));
        }
    }
}
