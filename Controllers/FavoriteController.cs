using ETicaret.Api.Models;
using ETicaret.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ETicaret.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly FavoriteService _service;
        private readonly ProductService _productService;

        public FavoriteController(
            FavoriteService service,
            ProductService productService)
        {
            _service = service;
            _productService = productService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetFavorites(string customerId)
        {
            var favorites = await _service.GetByCustomerIdAsync(customerId);

            var productIds = favorites
                .Select(f => f.ProductId)
                .ToList();

            var products = await _productService.GetByIdsAsync(productIds);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] Favorite request)
        {
            await _service.AddAsync(request.CustomerId, request.ProductId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFavorite([FromQuery] string customerId, [FromQuery] string productId)
        {
            await _service.RemoveAsync(customerId, productId);
            return Ok();
        }
    }
}