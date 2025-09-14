using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Viseralbug.Services;

namespace Viseralbug.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        private readonly FileUploadService _fileUploadService;
        private readonly ILogger<FileUploadController> _logger;

        public FileUploadController(FileUploadService fileUploadService, ILogger<FileUploadController> logger)
        {
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("No file provided");

                var fileName = await _fileUploadService.UploadFileAsync(file);
                var fileUrl = _fileUploadService.GetFileUrl(fileName);

                return Ok(new { fileName, fileUrl, message = "File uploaded successfully" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("File upload validation failed: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return StatusCode(500, "An error occurred while uploading the file");
            }
        }

        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("No file provided");

                // Validate that it's an image
                var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedImageExtensions.Contains(extension))
                    return BadRequest("Only image files are allowed for profile images");

                var fileName = await _fileUploadService.UploadFileAsync(file);
                var fileUrl = _fileUploadService.GetFileUrl(fileName);

                return Ok(new { fileName, fileUrl, message = "Profile image uploaded successfully" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Profile image upload validation failed: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading profile image");
                return StatusCode(500, "An error occurred while uploading the profile image");
            }
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            try
            {
                _fileUploadService.DeleteFile(fileName);
                return Ok(new { message = "File deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file: {FileName}", fileName);
                return StatusCode(500, "An error occurred while deleting the file");
            }
        }
    }
}
