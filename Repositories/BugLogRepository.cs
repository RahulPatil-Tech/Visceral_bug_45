using Microsoft.EntityFrameworkCore;
using Viseralbug.Data;
using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public class BugLogRepository : IBugLogRepository
    {
        private readonly ApplicationDbContext _context;
        public BugLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BugLog> GetByIdAsync(int id)
        {
            return await _context.BugLogs.FindAsync(id);
        }

        public async Task<IEnumerable<BugLog>> GetByBugIdAsync(int bugId)
        {
            return await _context.BugLogs.Where(bl => bl.BugId == bugId).ToListAsync();
        }

        public async Task AddAsync(BugLog bugLog)
        {
            await _context.BugLogs.AddAsync(bugLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BugLog bugLog)
        {
            _context.BugLogs.Update(bugLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bugLog = await _context.BugLogs.FindAsync(id);
            if (bugLog != null)
            {
                _context.BugLogs.Remove(bugLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
