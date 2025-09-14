using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Viseralbug.Models;
using Viseralbug.Services;
using System.Threading.Tasks;

namespace Viseralbug.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                return StatusCode(500, "An error occurred while retrieving users");
            }
        }

        [HttpGet("developers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDevelopers()
        {
            try
            {
                var developers = await _userService.GetByRoleAsync("Developer");
                return Ok(developers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving developers");
                return StatusCode(500, "An error occurred while retrieving developers");
            }
        }

        [HttpGet("testers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTesters()
        {
            try
            {
                var testers = await _userService.GetByRoleAsync("Tester");
                return Ok(testers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving testers");
                return StatusCode(500, "An error occurred while retrieving testers");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {UserId}", id);
                return StatusCode(500, "An error occurred while retrieving the user");
            }
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var user = await _userService.GetByUsernameAsync(username);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with username: {Username}", username);
                return StatusCode(500, "An error occurred while retrieving the user");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with email: {Email}", email);
                return StatusCode(500, "An error occurred while retrieving the user");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest("Username, Email, and Password are required");
                }

                // Check if user already exists
                if (await _userService.ExistsAsync(user.Username, user.Email))
                {
                    return BadRequest("User with this username or email already exists");
                }

                // Hash the password
                user.Password = PasswordHasher.HashPassword(user.Password);
                user.CreatedAt = DateTime.UtcNow;
                user.IsActive = true;

                await _userService.AddAsync(user);
                _logger.LogInformation("User created successfully: {Username}", user.Username);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user: {Username}", user.Username);
                return StatusCode(500, "An error occurred while creating the user");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest();
                await _userService.UpdateAsync(user);
                _logger.LogInformation("User updated successfully: {UserId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {UserId}", id);
                return StatusCode(500, "An error occurred while updating the user");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                _logger.LogInformation("User deleted successfully: {UserId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {UserId}", id);
                return StatusCode(500, "An error occurred while deleting the user");
            }
        }
    }
}
