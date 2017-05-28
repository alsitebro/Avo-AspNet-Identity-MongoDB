using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class EnsureWeCanExtendIdentityRoleTests : UserIntegrationTestsBase
	{
		private RoleManager<ExtendedIdentityRole> _manager;
		private ExtendedIdentityRole _role;

		public class ExtendedIdentityRole : IdentityRole
		{
            public ExtendedIdentityRole(string name) : base(name)
            {
            }

            public string ExtendedField { get; set; }
		}

		[SetUp]
		public void BeforeEachTestAfterBase()
		{
			var roles = Database.GetCollection<ExtendedIdentityRole>("roles");
			var roleStore = new RoleStore<ExtendedIdentityRole>(roles);
			_manager = new RoleManager<ExtendedIdentityRole>(roleStore);
			_role = new ExtendedIdentityRole("admin");
		}

		[Test]
		public void Create_ExtendedRoleType_SavesExtraFields()
		{
			_role.ExtendedField = "extendedField";

			_manager.Create(_role);
		    var roles = Database.GetCollection<ExtendedIdentityRole>("roles");
			var savedRole = roles.AsQueryable().Single();
			Expect(savedRole.ExtendedField, Is.EqualTo("extendedField"));
		}

		[Test]
		public void Create_ExtendedRoleType_ReadsExtraFields()
		{
			_role.ExtendedField = "extendedField";

			_manager.Create(_role);

			var savedRole = _manager.FindById(_role.Id);
			Expect(savedRole.ExtendedField, Is.EqualTo("extendedField"));
		}
	}
}