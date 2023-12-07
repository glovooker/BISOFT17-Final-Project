using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProyectoDiseñoBackend.Modelos
{
    public class Billing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("invoice_id")]
        public int invoiceId { get; set; }

        [BsonElement("project_id")]
        public int projectId { get; set; }

        [BsonElement("client_id")]
        public int clientId { get; set; }

        [BsonElement("account_id")]
        public int accountId { get; set; }

        [BsonElement("amount")]
        public double amount { get; set; }

        [BsonElement("issue_date")]
        public DateTime issueDate { get; set; }

        [BsonElement("due_date")]
        public DateTime dueDate { get; set; }

        [BsonElement("payment_status")]
        public string paymentStatus { get; set; } = null!;
    }
}
