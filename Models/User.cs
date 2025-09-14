using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Viseralbug.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<Project> DeveloperProjects { get; set; } = new List<Project>();
        public virtual ICollection<Project> TesterProjects { get; set; } = new List<Project>();
    }
}
