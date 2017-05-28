using System.Linq;
using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class EnsureWeCanExtendIdentityUserTests : UserIntegrationTestsBase
	{
		private UserManager<ExtendedIdentityUser> _Manager;
		private ExtendedIdentityUser _User;

		public class ExtendedIdentityUser : IdentityUser
		{
            public ExtendedIdentityUser(string username) : base(username)
            {
            }

            public string ExtendedField { get; set; }
		}

		[SetUp]
		public void BeforeEachTestAfterBase()
		{
			var users = Database.GetCollection<ExtendedIdentityUser>("users");
			var userStore = new UserStore<ExtendedIdentityUser>(users);
			_Manager = new UserManager<ExtendedIdentityUser>(userStore);
			_User = new ExtendedIdentityUser("bob");
		}

		[Test]
		public void Create_ExtendedUserType_SavesExtraFields()
		{
			_User.ExtendedField = "extendedField";

			_Manager.Create(_User);

			var savedUser = Database.GetCollection<ExtendedIdentityUser>("users").AsQueryable().Single();
			Expect(savedUser.ExtendedField, Is.EqualTo("extendedField"));
		}

		[Test]
		public void Create_ExtendedUserType_ReadsExtraFields()
		{
			_User.ExtendedField = "extendedField";

			_Manager.Create(_User);

			var savedUser = _Manager.FindById(_User.Id);
			Expect(savedUser.ExtendedField, Is.EqualTo("extendedField"));
		}
	}
}