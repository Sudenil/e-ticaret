using ETicaret.Api.Models;
using ETicaret.Api.Providers;
using MongoDB.Driver;

namespace ETicaret.Api.Services
{
    public class ProductService
    {
        private readonly MongoDbProvider _provider;

        public ProductService(MongoDbProvider provider)
        {
            _provider = provider;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _provider.Products.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _provider.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<Product> CreateAsync(Product product)
        {
            await _provider.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<List<Product>> SearchAsync(string? category, string? keyword)
        {
            var filterBuilder = Builders<Product>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(category))
            {
                filter &= filterBuilder.Regex(p => p.Category, new MongoDB.Bson.BsonRegularExpression($"^{category}$", "i"));
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                filter &= filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(keyword, "i"));
            }

            return await _provider.Products.Find(filter).ToListAsync();
        }
    }
}