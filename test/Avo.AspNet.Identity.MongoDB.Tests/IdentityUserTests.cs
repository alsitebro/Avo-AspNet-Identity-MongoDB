using Avo.AspNet.Identity.MongoDB;
using MongoDB.Bson;
using NUnit.Framework;

namespace Avo.AspNet.Identity.MongoDB.Tests
{
    [TestFixture]
	public class IdentityUserTests : UserIntegrationTestsBase
	{
		[Test]
		public void Insert_NoId_SetsId()
		{
			var user = new IdentityUser("demo");

			Users.InsertOne(user);

			Expect(user.Id, Is.Not.Null);
			Expect(ObjectId.Parse(user.Id), Is.Not.EqualTo(ObjectId.Empty));
		}
	}
}