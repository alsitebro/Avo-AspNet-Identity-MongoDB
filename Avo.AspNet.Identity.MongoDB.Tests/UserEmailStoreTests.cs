﻿using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class UserEmailStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public void Create_NewUser_HasNoEmail()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserManager();
			manager.Create(user);

			var email = manager.GetEmail(user.Id);

			Assert.That(email, Is.Null);
		}

		[Test]
		public void SetEmail_SetsEmail()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserManager();
			manager.Create(user);

			manager.SetEmail(user.Id, "email");

			Assert.That(manager.GetEmail(user.Id), Is.EqualTo("email"));
		}

		[Test]
		public void FindUserByEmail_ReturnsUser()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserManager();
			manager.Create(user);
			Assert.That(manager.FindByEmail("email"), Is.Null);

			manager.SetEmail(user.Id, "email");

			Assert.That(manager.FindByEmail("email"), Is.Not.Null);
		}

		[Test]
		public void Create_NewUser_IsNotEmailConfirmed()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);

			var isConfirmed = manager.IsEmailConfirmed(user.Id);

			Assert.That(isConfirmed, Is.False);
		}

		[Test]
		public void SetEmailConfirmed_IsConfirmed()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();
			var token = manager.GenerateEmailConfirmationToken(user.Id);

			manager.ConfirmEmail(user.Id, token);

			var isConfirmed = manager.IsEmailConfirmed(user.Id);
			Assert.That(isConfirmed);
		}

		[Test]
		public void ChangeEmail_AfterConfirmedOriginalEmail_NotEmailConfirmed()
		{
			var manager = GetUserManager();
			var user = new IdentityUser("bob");
			manager.Create(user);
			manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();
			var token = manager.GenerateEmailConfirmationToken(user.Id);
			manager.ConfirmEmail(user.Id, token);

			manager.SetEmail(user.Id, "new@email.com");

			var isConfirmed = manager.IsEmailConfirmed(user.Id);
			Assert.That(isConfirmed, Is.False);
		}
	}
}