using Microsoft.AspNetCore.Mvc;
using ETicaret.Api.Services;

namespace ETicaret.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var customer = await _service.RegisterAsync(request.FullName, request.Email, request.Password);
                return Ok(new { customer.Id, customer.FullName, customer.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var customer = await _service.LoginAsync(request.Email, request.Password);
            if (customer == null)
            {
                return Unauthorized(new { message = "E-posta veya şifre hatalı." });
            }

            return Ok(new { customer.Id, customer.FullName, customer.Email });
        }
    }

    public class RegisterRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}