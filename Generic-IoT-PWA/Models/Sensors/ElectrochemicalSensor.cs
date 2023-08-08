using Generic_IoT_PWA.Models.Abstracts.Entities;
using Generic_IoT_PWA.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Generic_IoT_PWA.Models.Sensors
{
    public class ElectrochemicalSensor : EditableEntity, ISensor
    {
        [BsonElement("device_id")]
        [BsonRequired]
        public Guid DeviceId { get; set; }
        [BsonElement("start_date")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }
        [BsonElement("end_date")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDate { get; set; }
        [BsonElement("measurements")]
        public Dictionary<DateTime,double> Measurements { get; set; }
        [BsonElement("transaction_count")]
        public int TransactionCount { get; set; }
        [BsonElement("sum_electrochemical_reading")]
        public double Sum { get; set; }
    }

    internal class ElectrochemicalTypes : IElectrochemical
    {
        [BsonElement("electrochemical_name")]
        public string? ElectrochemicalName {get; set;}
        [BsonElement("electrochemical_description")]
        public string? ElectrochemicalDescription { get; set; }
    }
}
