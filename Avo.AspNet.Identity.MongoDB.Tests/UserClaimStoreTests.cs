using System.Linq;
using System.Security.Claims;
using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class UserClaimStoreTests : UserIntegrationTestsBase
	{
		[Test]
		public void Create_NewUser_HasNoClaims()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserClaimManager();
			manager.Create(user);

			var claims = manager.GetClaims(user.Id);

			Assert.That(claims, Is.Empty);
		}

		[Test]
		public void AddClaim_ReturnsClaim()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserClaimManager();
			manager.Create(user);

			manager.AddClaim(user.Id, new Claim("type", "value"));

			var claim = manager.GetClaims(user.Id).Single();
			Assert.That(claim.Type, Is.EqualTo("type"));
			Assert.That(claim.Value, Is.EqualTo("value"));
		}

		[Test]
		public void RemoveClaim_RemovesExistingClaim()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserClaimManager();
			manager.Create(user);
			manager.AddClaim(user.Id, new Claim("type", "value"));

			manager.RemoveClaim(user.Id, new Claim("type", "value"));

			Assert.That(manager.GetClaims(user.Id), Is.Empty);
		}

		[Test]
		public void RemoveClaim_DifferentType_DoesNotRemoveClaim()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserClaimManager();
			manager.Create(user);
			manager.AddClaim(user.Id, new Claim("type", "value"));

			manager.RemoveClaim(user.Id, new Claim("otherType", "value"));

			Assert.That(manager.GetClaims(user.Id), Is.Not.Empty);
		}

		[Test]
		public void RemoveClaim_DifferentValue_DoesNotRemoveClaim()
		{
			var user = new IdentityUser("bob");
			var manager = GetUserClaimManager();
			manager.Create(user);
			manager.AddClaim(user.Id, new Claim("type", "value"));

			manager.RemoveClaim(user.Id, new Claim("type", "otherValue"));

			Assert.That(manager.GetClaims(user.Id), Is.Not.Empty);
		}
	}
}