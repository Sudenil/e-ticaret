using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETicaret.Api.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = null!;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}