using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
