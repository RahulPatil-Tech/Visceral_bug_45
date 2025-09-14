using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public interface IBugRepository
    {
        Task<Bug> GetByIdAsync(int id);
        Task<IEnumerable<Bug>> GetAllAsync();
        Task<IEnumerable<Bug>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<Bug>> GetByAssignedToIdAsync(int assignedToId);
        Task AddAsync(Bug bug);
        Task UpdateAsync(Bug bug);
        Task DeleteAsync(int id);
    }
}
