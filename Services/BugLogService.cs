using Viseralbug.Models;
using Viseralbug.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Services
{
    public class BugLogService
    {
        private readonly IBugLogRepository _bugLogRepository;
        public BugLogService(IBugLogRepository bugLogRepository)
        {
            _bugLogRepository = bugLogRepository;
        }

        public async Task<BugLog> GetByIdAsync(int id) => await _bugLogRepository.GetByIdAsync(id);
        public async Task<IEnumerable<BugLog>> GetByBugIdAsync(int bugId) => await _bugLogRepository.GetByBugIdAsync(bugId);
        public async Task AddAsync(BugLog bugLog) => await _bugLogRepository.AddAsync(bugLog);
        public async Task UpdateAsync(BugLog bugLog) => await _bugLogRepository.UpdateAsync(bugLog);
        public async Task DeleteAsync(int id) => await _bugLogRepository.DeleteAsync(id);
    }
}
