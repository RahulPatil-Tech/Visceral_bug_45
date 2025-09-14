using Viseralbug.Models;
using Viseralbug.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> GetByIdAsync(int id) => await _projectRepository.GetByIdAsync(id);
        public async Task<IEnumerable<Project>> GetAllAsync() => await _projectRepository.GetAllAsync();
        public async Task AddAsync(Project project) => await _projectRepository.AddAsync(project);
        public async Task UpdateAsync(Project project) => await _projectRepository.UpdateAsync(project);
        public async Task DeleteAsync(int id) => await _projectRepository.DeleteAsync(id);
        public async Task AssignUserAsync(int projectId, int userId, string role) => await _projectRepository.AssignUserAsync(projectId, userId, role);
        public async Task UnassignUserAsync(int projectId, int userId, string role) => await _projectRepository.UnassignUserAsync(projectId, userId, role);
    }
}
