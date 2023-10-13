using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Represents user accounts in the transport management system.
namespace TransportManagmentSystemAPI.Models
{
    public class UserManagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nic { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
