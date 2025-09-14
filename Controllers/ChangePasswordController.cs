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
    public class ChangePasswordController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<ChangePasswordController> _logger;

        public ChangePasswordController(UserService userService, ILogger<ChangePasswordController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var user = await _userService.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    _logger.LogWarning("Password change attempted for non-existent user ID: {UserId}", request.UserId);
                    return NotFound("User not found");
                }

                if (user.Password != request.OldPassword)
                {
                    _logger.LogWarning("Password change failed - incorrect old password for user ID: {UserId}", request.UserId);
                    return BadRequest("Old password is incorrect");
                }

                user.Password = request.NewPassword;
                await _userService.UpdateAsync(user);
                _logger.LogInformation("Password changed successfully for user ID: {UserId}", request.UserId);
                return Ok("Password changed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user ID: {UserId}", request.UserId);
                return StatusCode(500, "An error occurred while changing the password");
            }
        }
    }

    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
