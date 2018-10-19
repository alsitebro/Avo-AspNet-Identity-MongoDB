using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    public class UserIntegrationTestsBase
    {
        protected IMongoDatabase Database;
        protected IMongoCollection<IdentityUser> Users;
        protected IMongoCollection<IdentityRole> Roles;
        protected IMongoClient Client = new MongoClient("mongodb://localhost:27017");

        [SetUp]
        public void BeforeEachTest()
        {
            Database = Client.GetDatabase("identity-testing");
            Users = Database.GetCollection<IdentityUser>("users");
            Roles = Database.GetCollection<IdentityRole>("roles");
        }

        [TearDown]
        public void AfterTest()
        {
            Database.DropCollection("users");
            Database.DropCollection("roles");
            Client.DropDatabase("identity-testing");
        }

        protected UserManager<IdentityUser> GetUserManager()
        {
            return GetIdentityUserManager();
        }

        protected UserManager<IdentityUser> GetUserClaimManager()
        {
            return GetIdentityUserManager();
        }

        protected UserManager<IdentityUser> GetUserRoleManager()
        {
            return GetIdentityUserManager();
        }

        protected UserManager<IdentityUser> GetIdentityUserManager()
        {
            var store = new IdentityStore<IdentityUser>(Users);
            return new UserManager<IdentityUser>(store);
        }

        protected RoleManager<IdentityRole> GetRoleManager()
        {
            var store = new RoleStore<IdentityRole>(Roles);
            return new RoleManager<IdentityRole>(store);
        }
    }
}