using Microsoft.AspNetCore.Mvc;
using ETicaret.Api.Models;
using ETicaret.Api.Services;

namespace ETicaret.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var (items, totalCount) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new { items, totalCount });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? category,
            [FromQuery] string? subCategory,
            [FromQuery] string? type,
            [FromQuery] string? color,
            [FromQuery] string? size,
            [FromQuery] string? keyword)
        {
            var products = await _service.SearchAsync(
                category,
                subCategory,
                type,
                color,
                size,
                keyword);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}