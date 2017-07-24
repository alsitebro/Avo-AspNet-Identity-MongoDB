using System.Linq;
using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class UserLoginStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public void AddLogin_NewLogin_Adds()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser("bob");
			manager.Create(user);

			manager.AddLogin(user.Id, login);

			var savedLogin = Users.AsQueryable().Single().Logins.Single();
			Assert.That(savedLogin.LoginProvider, Is.EqualTo("provider"));
			Assert.That(savedLogin.ProviderKey, Is.EqualTo("key"));
		}


		[Test]
		public void RemoveLogin_NewLogin_Removes()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			manager.RemoveLogin(user.Id, login);

			var savedUser = Users.AsQueryable().Single();
			Assert.That(savedUser.Logins, Is.Empty);
		}

		[Test]
		public void GetLogins_OneLogin_ReturnsLogin()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var logins = manager.GetLogins(user.Id);

			var savedLogin = logins.Single();
			Assert.That(savedLogin.LoginProvider, Is.EqualTo("provider"));
			Assert.That(savedLogin.ProviderKey, Is.EqualTo("key"));
		}

		[Test]
		public void Find_UserWithLogin_FindsUser()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var findUser = manager.Find(login);

			Assert.That(findUser, Is.Not.Null);
		}

		[Test]
		public void Find_UserWithDifferentKey_DoesNotFindUser()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var findUser = manager.Find(new UserLoginInfo("provider", "otherkey"));

			Assert.That(findUser, Is.Null);
		}

		[Test]
		public void Find_UserWithDifferentProvider_DoesNotFindUser()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var findUser = manager.Find(new UserLoginInfo("otherprovider", "key"));

			Assert.That(findUser, Is.Null);
		}
	}
}