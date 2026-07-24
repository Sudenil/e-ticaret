using ETicaret.Api.Models;
using ETicaret.Api.Providers;
using MongoDB.Driver;

namespace ETicaret.Api.Services
{
    public class FavoriteService
    {
        private readonly MongoDbProvider _provider;

        public FavoriteService(MongoDbProvider provider)
        {
            _provider = provider;
        }

        public async Task<List<Favorite>> GetByCustomerIdAsync(string customerId)
        {
            return await _provider.Favorites
                .Find(f => f.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddAsync(string customerId, string productId)
        {
            var exists = await _provider.Favorites.Find(f =>
                f.CustomerId == customerId &&
                f.ProductId == productId).FirstOrDefaultAsync();

            if (exists == null)
            {
                await _provider.Favorites.InsertOneAsync(new Favorite
                {
                    CustomerId = customerId,
                    ProductId = productId
                });
            }
        }

        public async Task RemoveAsync(string customerId, string productId)
        {
            await _provider.Favorites.DeleteOneAsync(f =>
                f.CustomerId == customerId &&
                f.ProductId == productId);
        }
    }
}