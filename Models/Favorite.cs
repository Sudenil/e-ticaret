using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETicaret.Api.Models
{
    public class Favorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customerId")]
        public string CustomerId { get; set; } = null!;

        [BsonElement("productId")]
        public string ProductId { get; set; } = null!;
    }
}