using MongoDB.Bson.Serialization.Attributes;

namespace Generic_IoT_PWA.Models.Interfaces
{
    public interface IElectrochemical
    {
        [BsonElement("electrochemical_name")]
        public string? ElectrochemicalName { get; set; }
        [BsonElement("electrochemical_description")]
        public string? ElectrochemicalDescription { get; set; }
    }
}
