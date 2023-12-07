using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProyectoDiseñoBackend.Modelos
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("project_id")]
        public int projectId { get; set; }

        [BsonElement("name")]
        public string name { get; set; } = null!;

        [BsonElement("description")]
        public string description { get; set; } = null!;

        [BsonElement("status")]
        public string status { get; set; } = null!;

        [BsonElement("start_date")]
        public DateTime startDate { get; set; }

        [BsonElement("budget")]
        public double budget { get; set; }
    }
}
