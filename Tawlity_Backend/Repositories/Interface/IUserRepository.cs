using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
      public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        void DeleteUser(User user);
        Task<bool> SaveChangesAsync();
    }
}
