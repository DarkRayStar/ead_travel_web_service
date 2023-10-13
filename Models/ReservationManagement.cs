using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Represents a reservation in the transport management system.
namespace TransportManagmentSystemAPI.Models
{
    public class ReservationManagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ReferenceId { get; set; }
        public string TravallerName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string TravallerProfile { get; set; }
        public string PhoneNumber { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Train { get; set; }
        public int NoOfPassenger { get; set; }
        public string EmailAddress { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime BookingCreatedDate { get; set; }
        public bool IsCancelled { get; set; }

        [BsonIgnore]
        public TrainManagement BookedTrain { get; set; }

        [BsonIgnore]
        public TravallerManagement BookedTravallerProfile { get; set; }
    }
}
