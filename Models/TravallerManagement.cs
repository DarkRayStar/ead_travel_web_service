using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

// Represents traveler profiles in the transport management system.
namespace TransportManagmentSystemAPI.Models
{
    public class TravallerManagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool AccStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        [BsonIgnore]
        public UserManagement UserInfo { get; set; }

    }
}
