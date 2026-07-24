using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETicaret.Api.Models
{
    public class CartItem
    {
        [BsonElement("productId")]
        public string ProductId { get; set; } = null!;

        [BsonElement("productName")]
        public string ProductName { get; set; } = null!;

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("size")]
        public string? Size { get; set; }

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }
    }

    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customerId")]
        public string CustomerId { get; set; } = null!;

        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new();

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
    
}