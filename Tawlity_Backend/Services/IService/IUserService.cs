using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UsersDto>> GetAllUsersAsync();
        Task<UsersDto?> GetUserByIdAsync(int id);
        Task AddUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
