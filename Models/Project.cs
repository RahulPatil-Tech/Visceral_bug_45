using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Viseralbug.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<User> Developers { get; set; } = new List<User>();
        public virtual ICollection<User> Testers { get; set; } = new List<User>();
    }
}
