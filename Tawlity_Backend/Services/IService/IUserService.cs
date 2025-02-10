using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<List<FavoriteDto>> GetUserFavoritesAsync(int userId);
    }
}
