using Tawlity_Backend.Dtos;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserDto
            {
                Id = u.EmployeeId,
                Name=u.EmployeeName,
                Email = u.EmployeeEmail,
                Role = u.Employee_Role.ToString() // Assuming you have a Role navigation property
            }).ToList();
        }

        
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.EmployeeId,
                Name = $"{user.EmployeeName}",
                Email = user.EmployeeEmail,
                Role = user.Employee_Role.ToString()
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return false;

            user.EmployeeName = userDto.Name;
            user.EmployeeEmail = userDto.Email;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<List<FavoriteDto>> GetUserFavoritesAsync(int userId)
        {
            var favorites = await _userRepository.GetUserFavoritesAsync(userId);
            return favorites.Select(f => new FavoriteDto
            {
                Id = f.Id,
                RestaurantName = f.Restaurant?.Name,
                MenuItemName = f.MenuItem?.Name,
                AddedOn = f.AddedOn
            }).ToList();
        }
    }
}

