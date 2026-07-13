using ETicaret.Api.Models;
using ETicaret.Api.Providers;
using MongoDB.Driver;

namespace ETicaret.Api.Services
{
    public class CustomerService
    {
        private readonly MongoDbProvider _provider;

        public CustomerService(MongoDbProvider provider)
        {
            _provider = provider;
        }

        public async Task<Customer?> GetByEmailAsync(string email) =>
            await _provider.Customers.Find(c => c.Email == email).FirstOrDefaultAsync();

        public async Task<Customer> RegisterAsync(string fullName, string email, string password)
        {
            var existing = await GetByEmailAsync(email);
            if (existing != null)
            {
                throw new Exception("Bu e-posta ile zaten bir kayıt var.");
            }

            var customer = new Customer
            {
                FullName = fullName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _provider.Customers.InsertOneAsync(customer);
            return customer;
        }

        public async Task<Customer?> LoginAsync(string email, string password)
        {
            var customer = await GetByEmailAsync(email);
            if (customer == null) return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash);
            return isValid ? customer : null;
        }
    }
}