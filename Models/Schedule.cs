using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Represents a schedule for train departures in the transport management system.
namespace TransportManagmentSystemAPI.Models
{
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Starttime { get; set; }
        public string Day { get; set; }
        public string StartStationName { get; set; }
        public string EndStationName { get; set;}

    }
}
