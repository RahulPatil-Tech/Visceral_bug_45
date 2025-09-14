using Microsoft.EntityFrameworkCore;
using Viseralbug.Data;
using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public class TaskLogRepository : ITaskLogRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskLog> GetByIdAsync(int id)
        {
            return await _context.TaskLogs.FindAsync(id);
        }

        public async Task<IEnumerable<TaskLog>> GetByTaskIdAsync(int taskId)
        {
            return await _context.TaskLogs.Where(tl => tl.TaskId == taskId).ToListAsync();
        }

        public async Task AddAsync(TaskLog taskLog)
        {
            await _context.TaskLogs.AddAsync(taskLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskLog taskLog)
        {
            _context.TaskLogs.Update(taskLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var taskLog = await _context.TaskLogs.FindAsync(id);
            if (taskLog != null)
            {
                _context.TaskLogs.Remove(taskLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
