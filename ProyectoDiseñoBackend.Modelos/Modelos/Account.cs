using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProyectoDiseñoBackend.Modelos
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("admin_id")]
        public int adminId { get; set; }

        [BsonElement("region")]
        public string region { get; set; } = null!;

        [BsonElement("account_id")]
        public int accountId { get; set; }

        [BsonElement("budget_anual")]
        public double budgetAnual { get; set; }
    }
}
