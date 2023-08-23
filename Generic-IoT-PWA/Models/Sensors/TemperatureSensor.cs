using Generic_IoT_PWA.Models.Abstracts.Entities;
using Generic_IoT_PWA.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Generic_IoT_PWA.Models.Sensors
{
    public class TemperatureSensor : EditableEntity, ITemperature, ISensor
    {
        [BsonElement("device_id")]
        [BsonRequired]
        public Guid DeviceId { get; set; }
        [BsonElement("start_date")]
        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }
        [BsonElement("end_date")]
        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDate { get; set; }
        public Dictionary<DateTime, double>? Measurements { get; set; }
        [BsonElement("transaction_count")]
        public int TransactionCount { get; set; }
        [BsonElement("sum_temperature")]
        public double Sum { get; set; }
    }
}
