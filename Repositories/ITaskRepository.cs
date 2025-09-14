using Viseralbug.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Repositories
{
    public interface ITaskRepository
    {
        Task<WorkTask> GetByIdAsync(int id);
        Task<IEnumerable<WorkTask>> GetAllAsync();
        Task<IEnumerable<WorkTask>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<WorkTask>> GetByAssignedToIdAsync(int assignedToId);
        Task AddAsync(WorkTask task);
        Task UpdateAsync(WorkTask task);
        Task DeleteAsync(int id);
    }
}
