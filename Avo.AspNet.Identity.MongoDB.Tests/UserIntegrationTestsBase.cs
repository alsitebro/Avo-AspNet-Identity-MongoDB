using Avo.AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    public class UserIntegrationTestsBase: AssertionHelper
    {
        protected IMongoDatabase Database;
        protected IMongoCollection<IdentityUser> Users;
        protected IMongoCollection<IdentityRole> Roles;
        

        [SetUp]
        public void BeforeEachTest()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var identityTesting = "identity-testing";

           
            Database = client.GetDatabase(identityTesting);
            Users = Database.GetCollection<IdentityUser>("users");
            Roles = Database.GetCollection<IdentityRole>("roles");

            Database.DropCollection("users");
            Database.DropCollection("roles");
        }

        protected UserManager<IdentityUser> GetUserManager()
        {
            var store = new UserStore<IdentityUser>(Users);
            return new UserManager<IdentityUser>(store);
        }

        protected UserManager<IdentityUser> GetUserClaimManager()
        {
            var store = new UserClaimStore<IdentityUser>(Users);
            return new UserManager<IdentityUser>(store);
        }

        protected UserManager<IdentityUser> GetUserRoleManager()
        {
            var store = new UserRoleStore<IdentityUser>(Users);
            return new UserManager<IdentityUser>(store);
        }

        protected RoleManager<IdentityRole> GetRoleManager()
        {
            var store = new RoleStore<IdentityRole>(Roles);
            return new RoleManager<IdentityRole>(store);
        }
    }
}
