using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETicaret.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }

        // Giyim, Elektronik, Ev Eşyası
        [BsonElement("category")]
        public string Category { get; set; } = null!;

        // Kadın, Erkek
        [BsonElement("subCategory")]
        public string? SubCategory { get; set; }

        // Pantolon, Elbise, Ayakkabı
        [BsonElement("type")]
        public string? Type { get; set; }

        // Jean, Kumaş, Günlük, Abiye...
        [BsonElement("style")]
        public string? Style { get; set; }

        // Mavi, Siyah, Beyaz...
        [BsonElement("color")]
        public string? Color { get; set; }

        // 36, 38, 40, 42...
        [BsonElement("sizes")]
        public List<string> Sizes { get; set; } = new();

        [BsonElement("stock")]
        public int Stock { get; set; }

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}