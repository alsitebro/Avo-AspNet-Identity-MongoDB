using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class UserRoleStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public void GetRoles_UserHasNoRoles_ReturnsNoRoles()
		{
			var manager = GetUserRoleManager();
			var user = new IdentityUser("bob");
			manager.Create(user);

			var roles = manager.GetRoles(user.Id);

			Assert.That(roles, Is.Empty);
		}

		[Test]
		public void AddRole_Adds()
		{
			var manager = GetUserRoleManager();
			var user = new IdentityUser("bob");
			manager.Create(user);

			manager.AddToRole(user.Id, "role");

			var savedUser = Users.AsQueryable().Single();
			Assert.That(savedUser.Roles, Is.EquivalentTo(new[] {"role"}));
			Assert.That(manager.IsInRole(user.Id, "role"), Is.True);
		}

		[Test]
		public void RemoveRole_Removes()
		{
			var manager = GetUserRoleManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.AddToRole(user.Id, "role");

			manager.RemoveFromRole(user.Id, "role");

			var savedUser = Users.AsQueryable().Single();
			Assert.That(savedUser.Roles, Is.Empty);
			Assert.That(manager.IsInRole(user.Id, "role"), Is.False);
		}
	}
}