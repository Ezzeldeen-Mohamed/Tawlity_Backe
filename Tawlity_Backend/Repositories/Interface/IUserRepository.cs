using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
      public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserProfileByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        void DeleteUser(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> SaveChangesAsync();
    }
}
