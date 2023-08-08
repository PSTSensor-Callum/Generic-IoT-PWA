using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Generic_IoT_PWA.Models.Abstracts.Entities
{

    public abstract class Entity
    {
        [BsonElement("sensor_id")]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        public Entity() { }
    }

}
