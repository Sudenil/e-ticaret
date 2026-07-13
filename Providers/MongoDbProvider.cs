using MongoDB.Driver;
using ETicaret.Api.Models;
using Microsoft.Extensions.Options;

namespace ETicaret.Api.Providers
{
    public class MongoDbProvider
    {
        private readonly IMongoDatabase _database;

        public MongoDbProvider(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Product> Products =>
            _database.GetCollection<Product>("Products");

        public IMongoCollection<Customer> Customers =>
            _database.GetCollection<Customer>("Customers");
        public IMongoCollection<Cart> Carts =>
            _database.GetCollection<Cart>("Carts");
    }
}