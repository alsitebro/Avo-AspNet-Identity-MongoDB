using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Avo.AspNet.Identity.MongoDB
{
    public class IdentityRole : IRole<string>
    {
        public IdentityRole(string name)
        {
            Name = name;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
