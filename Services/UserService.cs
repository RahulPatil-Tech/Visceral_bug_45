using Viseralbug.Models;
using Viseralbug.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viseralbug.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);
        public async Task<User> GetByUsernameAsync(string username) => await _userRepository.GetByUsernameAsync(username);
        public async Task<User> GetByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);
        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();
        public async Task<IEnumerable<User>> GetByRoleAsync(string role) => await _userRepository.GetByRoleAsync(role);
        public async Task<bool> ExistsAsync(string username, string email) => await _userRepository.ExistsAsync(username, email);
        public async Task AddAsync(User user) => await _userRepository.AddAsync(user);
        public async Task UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
        public async Task DeleteAsync(int id) => await _userRepository.DeleteAsync(id);
    }
}
