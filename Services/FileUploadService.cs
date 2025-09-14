using Microsoft.Extensions.Options;
using Viseralbug.Models;

namespace Viseralbug.Services
{
    public class FileUploadService
    {
        private readonly FileUploadSettings _settings;
        private readonly ILogger<FileUploadService> _logger;

        public FileUploadService(IOptions<FileUploadSettings> settings, ILogger<FileUploadService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            // Validate file size
            if (file.Length > _settings.MaxFileSizeInMB * 1024 * 1024)
                throw new ArgumentException($"File size exceeds maximum allowed size of {_settings.MaxFileSizeInMB}MB");

            // Validate file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_settings.AllowedExtensions.Contains(extension))
                throw new ArgumentException($"File extension {extension} is not allowed");

            // Create upload directory if it doesn't exist
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _settings.UploadPath);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Generate unique filename
            var fileName = $"{DateTime.UtcNow.Ticks}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _logger.LogInformation("File uploaded successfully: {FileName}", fileName);
            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _settings.UploadPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogInformation("File deleted successfully: {FileName}", fileName);
            }
        }

        public string GetFileUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            return $"/uploads/{fileName}";
        }
    }
}
