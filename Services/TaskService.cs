using Viseralbug.Models;
using Viseralbug.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<WorkTask> GetByIdAsync(int id) => await _taskRepository.GetByIdAsync(id);
        public async Task<IEnumerable<WorkTask>> GetAllAsync() => await _taskRepository.GetAllAsync();
        public async Task<IEnumerable<WorkTask>> GetByProjectIdAsync(int projectId) => await _taskRepository.GetByProjectIdAsync(projectId);
        public async Task<IEnumerable<WorkTask>> GetByAssignedToIdAsync(int assignedToId) => await _taskRepository.GetByAssignedToIdAsync(assignedToId);
        public async Task AddAsync(WorkTask task) => await _taskRepository.AddAsync(task);
        public async Task UpdateAsync(WorkTask task) => await _taskRepository.UpdateAsync(task);
        public async Task DeleteAsync(int id) => await _taskRepository.DeleteAsync(id);
    }
}
