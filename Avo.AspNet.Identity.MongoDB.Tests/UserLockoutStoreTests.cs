using System;
using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class UserLockoutStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public void AccessFailed_IncrementsAccessFailedCount()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 3;

			manager.AccessFailed(user.Id);

			Assert.That(manager.GetAccessFailedCount(user.Id), Is.EqualTo(1));
		}

		[Test]
		public void IncrementAccessFailedCount_ReturnsNewCount()
		{
			var store = new UserStore<IdentityUser>(null);
			var user = new IdentityUser("bob");

			var count = store.IncrementAccessFailedCountAsync(user);

			Assert.That(count.Result, Is.EqualTo(1));
		}

		[Test]
		public void ResetAccessFailed_AfterAnAccessFailed_SetsToZero()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 3;
			manager.AccessFailed(user.Id);

			manager.ResetAccessFailedCount(user.Id);

			Assert.That(manager.GetAccessFailedCount(user.Id), Is.EqualTo(0));
		}

		[Test]
		public void AccessFailed_NotOverMaxFailures_NoLockoutEndDate()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 3;

			manager.AccessFailed(user.Id);

			Assert.That(manager.GetLockoutEndDate(user.Id), Is.EqualTo(DateTimeOffset.MinValue));
		}

		[Test]
		public void AccessFailed_ExceedsMaxFailedAccessAttempts_LocksAccount()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 0;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromHours(1);

			manager.AccessFailed(user.Id);

			var lockoutEndDate = manager.GetLockoutEndDate(user.Id);
			Assert.That(lockoutEndDate.Subtract(DateTime.UtcNow).TotalHours, Is.GreaterThan(0.9).And.LessThan(1.1));
		}

		[Test]
		public void SetLockoutEnabled()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);

			manager.SetLockoutEnabled(user.Id, true);
			Assert.That(manager.GetLockoutEnabled(user.Id));

			manager.SetLockoutEnabled(user.Id, false);
			Assert.That(manager.GetLockoutEnabled(user.Id), Is.False);
		}
	}
}