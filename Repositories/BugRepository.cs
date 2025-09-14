using Microsoft.EntityFrameworkCore;
using Viseralbug.Data;
using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationDbContext _context;
        public BugRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bug> GetByIdAsync(int id)
        {
            return await _context.Bugs.FindAsync(id);
        }

        public async Task<IEnumerable<Bug>> GetAllAsync()
        {
            return await _context.Bugs.ToListAsync();
        }

        public async Task<IEnumerable<Bug>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Bugs.Where(b => b.ProjectId == projectId).ToListAsync();
        }

        public async Task<IEnumerable<Bug>> GetByAssignedToIdAsync(int assignedToId)
        {
            return await _context.Bugs.Where(b => b.AssignedToId == assignedToId).ToListAsync();
        }

        public async Task AddAsync(Bug bug)
        {
            await _context.Bugs.AddAsync(bug);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bug bug)
        {
            _context.Bugs.Update(bug);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug != null)
            {
                _context.Bugs.Remove(bug);
                await _context.SaveChangesAsync();
            }
        }
    }
}
