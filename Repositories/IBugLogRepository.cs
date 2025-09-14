using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public interface IBugLogRepository
    {
        Task<BugLog> GetByIdAsync(int id);
        Task<IEnumerable<BugLog>> GetByBugIdAsync(int bugId);
        Task AddAsync(BugLog bugLog);
        Task UpdateAsync(BugLog bugLog);
        Task DeleteAsync(int id);
    }
}
