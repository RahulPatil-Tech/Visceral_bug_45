using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public interface ITaskLogRepository
    {
        Task<TaskLog> GetByIdAsync(int id);
        Task<IEnumerable<TaskLog>> GetByTaskIdAsync(int taskId);
        Task AddAsync(TaskLog taskLog);
        Task UpdateAsync(TaskLog taskLog);
        Task DeleteAsync(int id);
    }
}
