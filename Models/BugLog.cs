using System.ComponentModel.DataAnnotations;

namespace Viseralbug.Models
{
    public class BugLog
    {
        [Key]
        public int Id { get; set; }
        public int BugId { get; set; }
        public string Status { get; set; }
        public string? Comment { get; set; }
        public int ChangedById { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
