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

        public async Task<List<Product>> SearchAsync(
            string? category,
            string? subCategory,
            string? type,
            string? color,
            string? size,
            string? keyword)
        {
            var filterBuilder = Builders<Product>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(category))
                filter &= filterBuilder.Regex(p => p.Category,
                    new MongoDB.Bson.BsonRegularExpression($"^{category}$", "i"));

            if (!string.IsNullOrWhiteSpace(subCategory))
                filter &= filterBuilder.Regex(p => p.SubCategory,
                    new MongoDB.Bson.BsonRegularExpression($"^{subCategory}$", "i"));

            if (!string.IsNullOrWhiteSpace(type))
                filter &= filterBuilder.Regex(p => p.Type,
                    new MongoDB.Bson.BsonRegularExpression($"^{type}$", "i"));

            if (!string.IsNullOrWhiteSpace(color))
                filter &= filterBuilder.Regex(p => p.Color,
                    new MongoDB.Bson.BsonRegularExpression($"^{color}$", "i"));

            if (!string.IsNullOrWhiteSpace(keyword))
                filter &= filterBuilder.Regex(p => p.Name,
                    new MongoDB.Bson.BsonRegularExpression(keyword, "i"));

            return await _provider.Products.Find(filter).ToListAsync();
        }

        // FAVORİLER İÇİN EKLENDİ
        public async Task<List<Product>> GetByIdsAsync(List<string> productIds)
        {
            return await _provider.Products
                .Find(p => productIds.Contains(p.Id!))
                .ToListAsync();
        }
        public async Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
{
    var totalCount = await _provider.Products.CountDocumentsAsync(_ => true);

    var items = await _provider.Products
        .Find(_ => true)
        .Skip((page - 1) * pageSize)
        .Limit(pageSize)
        .ToListAsync();

    return (items, totalCount);
}
    }
}