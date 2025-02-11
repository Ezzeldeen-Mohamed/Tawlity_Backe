using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.SomeThingsWeWillUseInTheFuther;

namespace Tawlity_Backend.Services.Interface
{
    public interface Login_IRepo
    {
        // with database direct
        Task<User?> GetEmployeeByEmailAsync(string email);
        Task AddEmployeeAsync(User employee);
        Task<User?> GetEmployeeByResetTokenAsync(string token);
        Task UpdateEmployeeAsync(User employee);
        Task SaveChangesAsync();
        Task<User?> GetUserByResetTokenAsync(string token);

    }
}

