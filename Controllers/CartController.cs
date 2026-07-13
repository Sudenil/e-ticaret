using Microsoft.AspNetCore.Mvc;
using ETicaret.Api.Services;

namespace ETicaret.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCart(string customerId)
        {
            var cart = await _service.GetByCustomerIdAsync(customerId);
            return Ok(cart);
        }

        [HttpPost("{customerId}/items")]
        public async Task<IActionResult> AddItem(string customerId, [FromBody] AddCartItemRequest request)
        {
            try
            {
                var cart = await _service.AddItemAsync(customerId, request.ProductId, request.Quantity);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{customerId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(string customerId, string productId)
        {
            var cart = await _service.RemoveItemAsync(customerId, productId);
            return Ok(cart);
        }
    }

    public class AddCartItemRequest
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
    }
}