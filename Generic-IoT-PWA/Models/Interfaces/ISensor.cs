using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Generic_IoT_PWA.Models.Interfaces
{
    public interface ISensor
    {
        [BsonElement("device_id")]
        public Guid DeviceId { get; set; }
        [BsonElement("start_date")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }
        [BsonElement("end_date")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDate { get; set; }
        [BsonElement("transaction_count")]
        public int TransactionCount { get; set; }
    }
}
