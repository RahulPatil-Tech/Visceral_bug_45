namespace Viseralbug.Models
{
    public class FileUploadSettings
    {
        public int MaxFileSizeInMB { get; set; }
        public string[] AllowedExtensions { get; set; } = Array.Empty<string>();
        public string UploadPath { get; set; } = string.Empty;
    }
}
