using Microsoft.EntityFrameworkCore;
using Viseralbug.Models;

namespace Viseralbug.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugLog> BugLogs { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Project and User for Developers
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Developers)
                .WithMany(u => u.DeveloperProjects)
                .UsingEntity(j => j.ToTable("ProjectDevelopers"));

            // Configure many-to-many relationship between Project and User for Testers
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Testers)
                .WithMany(u => u.TesterProjects)
                .UsingEntity(j => j.ToTable("ProjectTesters"));
        }
    }
}
