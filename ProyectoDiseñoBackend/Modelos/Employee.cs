using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProyectoDiseñoBackend.Modelos
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("employee_id")]
        public int employeeId { get; set; }

        [BsonElement("band_id")]
        public int bandId { get; set; }

        [BsonElement("name")]
        public string name { get; set; } = null!;

        [BsonElement("rol")]
        public string rol { get; set; } = null!;

        [BsonElement("project_id")]
        public int projectId { get; set; }

        [BsonElement("email")]
        public string email { get; set; } = null!;
    }
}
