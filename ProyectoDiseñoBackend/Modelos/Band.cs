using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProyectoDiseñoBackend.Modelos
{
    public class Band
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("band_id")]
        public int bandId { get; set; }

        [BsonElement("description")]
        public string description { get; set; } = null!;

        [BsonElement("price")]
        public double price { get; set; }
    }
}
