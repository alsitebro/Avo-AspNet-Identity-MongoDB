using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class UserTwoFactorStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public void SetTwoFactorEnabled()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserManager();
			manager.Create(user);

			manager.SetTwoFactorEnabled(user.Id, true);

			Expect(manager.GetTwoFactorEnabled(user.Id));
		}

		[Test]
		public void ClearTwoFactorEnabled_PreviouslyEnabled_NotEnabled()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserManager();
			manager.Create(user);
			manager.SetTwoFactorEnabled(user.Id, true);

			manager.SetTwoFactorEnabled(user.Id, false);

			Expect(manager.GetTwoFactorEnabled(user.Id), Is.False);
		}
	}
}