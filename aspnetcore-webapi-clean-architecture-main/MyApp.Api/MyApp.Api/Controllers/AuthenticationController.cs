using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Services;
using System.Threading.Tasks;
using System;

namespace MyApp.Web.Controllers
{
    [ApiController]
    [Route("api/hotel/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthenticationController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user == null || !_userRepository.VerifyPassword(user, request.Password))
                {
                    return Unauthorized("Invalid email or password.");
                }

                var token = _jwtService.GenerateToken(user);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}