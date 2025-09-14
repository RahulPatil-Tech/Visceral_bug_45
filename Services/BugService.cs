using Viseralbug.Models;
using Viseralbug.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Services
{
    public class BugService
    {
        private readonly IBugRepository _bugRepository;
        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public async Task<Bug> GetByIdAsync(int id) => await _bugRepository.GetByIdAsync(id);
        public async Task<IEnumerable<Bug>> GetAllAsync() => await _bugRepository.GetAllAsync();
        public async Task<IEnumerable<Bug>> GetByProjectIdAsync(int projectId) => await _bugRepository.GetByProjectIdAsync(projectId);
        public async Task<IEnumerable<Bug>> GetByAssignedToIdAsync(int assignedToId) => await _bugRepository.GetByAssignedToIdAsync(assignedToId);
        public async Task AddAsync(Bug bug) => await _bugRepository.AddAsync(bug);
        public async Task UpdateAsync(Bug bug) => await _bugRepository.UpdateAsync(bug);
        public async Task DeleteAsync(int id) => await _bugRepository.DeleteAsync(id);
    }
}
