using Microsoft.EntityFrameworkCore;
using Viseralbug.Data;
using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Viseralbug.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Developers)
                .Include(p => p.Testers)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Developers)
                .Include(p => p.Testers)
                .ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignUserAsync(int projectId, int userId, string role)
        {
            var project = await _context.Projects
                .Include(p => p.Developers)
                .Include(p => p.Testers)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            
            var user = await _context.Users.FindAsync(userId);
            
            if (project == null || user == null)
                throw new InvalidOperationException("Project or user not found");

            if (role.ToUpper() == "DEVELOPER")
            {
                if (!project.Developers.Any(d => d.Id == userId))
                {
                    project.Developers.Add(user);
                }
            }
            else if (role.ToUpper() == "TESTER")
            {
                if (!project.Testers.Any(t => t.Id == userId))
                {
                    project.Testers.Add(user);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task UnassignUserAsync(int projectId, int userId, string role)
        {
            var project = await _context.Projects
                .Include(p => p.Developers)
                .Include(p => p.Testers)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            
            if (project == null)
                throw new InvalidOperationException("Project not found");

            if (role.ToUpper() == "DEVELOPER")
            {
                var developer = project.Developers.FirstOrDefault(d => d.Id == userId);
                if (developer != null)
                {
                    project.Developers.Remove(developer);
                }
            }
            else if (role.ToUpper() == "TESTER")
            {
                var tester = project.Testers.FirstOrDefault(t => t.Id == userId);
                if (tester != null)
                {
                    project.Testers.Remove(tester);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
