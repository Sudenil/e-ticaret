using ETicaret.Api.Models;
using ETicaret.Api.Providers;
using MongoDB.Driver;

namespace ETicaret.Api.Services
{
    public class CartService
    {
        private readonly MongoDbProvider _provider;
        private readonly ProductService _productService;

        public CartService(MongoDbProvider provider, ProductService productService)
        {
            _provider = provider;
            _productService = productService;
        }

        public async Task<Cart> GetByCustomerIdAsync(string customerId)
        {
            var cart = await _provider.Carts.Find(c => c.CustomerId == customerId).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                await _provider.Carts.InsertOneAsync(cart);
            }

            return cart;
        }

        public async Task<Cart> AddItemAsync(string customerId, string productId, int quantity, string? size)
        {
            var product = await _productService.GetByIdAsync(productId);

            if (product == null)
            {
                throw new Exception("Ürün bulunamadı.");
            }

            var cart = await GetByCustomerIdAsync(customerId);

            // Aynı ürün ve aynı beden varsa miktarı artır
            var existingItem = cart.Items.FirstOrDefault(i =>
                i.ProductId == productId &&
                i.Size == size);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    Size = size,
                    ImageUrl = product.ImageUrl
                });
            }

            cart.UpdatedAt = DateTime.UtcNow;

            await _provider.Carts.ReplaceOneAsync(c => c.Id == cart.Id, cart);

            return cart;
        }

        public async Task<Cart> RemoveItemAsync(string customerId, string productId)
        {
            var cart = await GetByCustomerIdAsync(customerId);

            cart.Items.RemoveAll(i => i.ProductId == productId);

            cart.UpdatedAt = DateTime.UtcNow;

            await _provider.Carts.ReplaceOneAsync(c => c.Id == cart.Id, cart);

            return cart;
        }
    }
}