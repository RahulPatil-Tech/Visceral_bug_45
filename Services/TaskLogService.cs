using Viseralbug.Models;
using Viseralbug.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Services
{
    public class TaskLogService
    {
        private readonly ITaskLogRepository _taskLogRepository;
        public TaskLogService(ITaskLogRepository taskLogRepository)
        {
            _taskLogRepository = taskLogRepository;
        }

        public async Task<TaskLog> GetByIdAsync(int id) => await _taskLogRepository.GetByIdAsync(id);
        public async Task<IEnumerable<TaskLog>> GetByTaskIdAsync(int taskId) => await _taskLogRepository.GetByTaskIdAsync(taskId);
        public async Task AddAsync(TaskLog taskLog) => await _taskLogRepository.AddAsync(taskLog);
        public async Task UpdateAsync(TaskLog taskLog) => await _taskLogRepository.UpdateAsync(taskLog);
        public async Task DeleteAsync(int id) => await _taskLogRepository.DeleteAsync(id);
    }
}
