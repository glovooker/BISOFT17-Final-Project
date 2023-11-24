using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProyectoDiseñoBackend.Modelos
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("client_id")]
        public int clientId { get; set; }

        [BsonElement("name")]
        public string name { get; set; } = null!;

        [BsonElement("main_contact")]
        public string mainContact { get; set; } = null!;

        [BsonElement("email")]
        public string email { get; set; } = null!;
    }
}
