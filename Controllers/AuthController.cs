using Microsoft.AspNetCore.Mvc;
using Viseralbug.Models;
using Viseralbug.Services;
using System.Threading.Tasks;

namespace Viseralbug.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserService userService, JwtService jwtService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var existing = await _userService.GetByUsernameAsync(user.Username);
                if (existing != null)
                    return BadRequest("Username already exists");

                var existingEmail = await _userService.GetByEmailAsync(user.Email);
                if (existingEmail != null)
                    return BadRequest("Email already exists");

                // Hash the password before saving
                user.Password = PasswordHasher.HashPassword(user.Password);
                user.CreatedAt = DateTime.UtcNow;
                user.IsActive = true;

                await _userService.AddAsync(user);
                _logger.LogInformation("User registered successfully: {Username}", user.Username);
                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                return StatusCode(500, "An error occurred during registration");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Login attempt for username: {Username}", loginRequest.Username);
                
                if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    _logger.LogWarning("Login attempt with empty username or password");
                    return BadRequest("Username and password are required");
                }

                var user = await _userService.GetByUsernameAsync(loginRequest.Username);
                if (user == null)
                {
                    _logger.LogWarning("Login attempt for non-existent user: {Username}", loginRequest.Username);
                    return Unauthorized("Invalid credentials");
                }

                if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.Password))
                {
                    _logger.LogWarning("Failed login attempt for username: {Username} - Invalid password", loginRequest.Username);
                    return Unauthorized("Invalid credentials");
                }

                if (!user.IsActive)
                {
                    _logger.LogWarning("Login attempt for inactive user: {Username}", loginRequest.Username);
                    return Unauthorized("Account is deactivated");
                }

                var token = _jwtService.GenerateToken(user.Username, user.Role, user.Id);
                _logger.LogInformation("User logged in successfully: {Username}", user.Username);
                
                return Ok(new { token, user = new { user.Id, user.Username, user.Email, user.Role, user.Name, user.ProfileImage } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login for username: {Username}", loginRequest.Username);
                return StatusCode(500, "An error occurred during login");
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
