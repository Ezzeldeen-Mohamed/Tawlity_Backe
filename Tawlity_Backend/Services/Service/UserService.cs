using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<IEnumerable<UsersDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UsersDto
            {
                EmployeeId = u.EmployeeId,
                EmployeeName = u.EmployeeName,
                EmployeeGender = u.EmployeeGender,
                EmployeePhone = u.EmployeePhone,
                EmployeeEmail = u.EmployeeEmail,
                EmployeeCity = u.EmployeeCity,
                Employee_Role = u.Employee_Role,
                EmployeeCreditCard = u.EmployeeCreditCard
            });
        }

        public async Task<UsersDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? null : new UsersDto
            {
                EmployeeId = user.EmployeeId,
                EmployeeName = user.EmployeeName,
                EmployeeGender = user.EmployeeGender,
                EmployeePhone = user.EmployeePhone,
                EmployeeEmail = user.EmployeeEmail,
                EmployeeCity = user.EmployeeCity,
                Employee_Role = user.Employee_Role,
                EmployeeCreditCard = user.EmployeeCreditCard
            };
        }
        public async Task<UserProfileDto?> GetUserProfileByIdAsync(int id)
        {
            var user = await _userRepository.GetUserProfileByIdAsync(id);
            return user == null ? null : new UserProfileDto
            {
                FullName=user.EmployeeName,
                Email=user.EmployeeEmail,
                Address=user.EmployeeCity.ToString(),
                reservationForProfiles=user.Reservations.Select(x=>new ReservationForProfileDto
                {
                    ReservationDate=x.ReservationDate,
                    RestaurantName = x.Restaurant?.Name ?? "Unknown"
                }).ToList() ?? new List<ReservationForProfileDto>()
            };
        }

        public async Task AddUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                EmployeeName = dto.EmployeeName,
                EmployeeGender = dto.EmployeeGender,
                EmployeePhone = dto.EmployeePhone,
                EmployeeEmail = dto.EmployeeEmail,
                EmployeeCity = dto.EmployeeCity,
                Employee_Role = dto.Employee_Role,
                EmployeeCreditCard = dto.EmployeeCreditCard,
                EmployeePassword = dto.EmployeePassword
            };

            await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return false;

            user.EmployeeName = dto.EmployeeName ?? user.EmployeeName;
            user.EmployeePhone = dto.EmployeePhone ?? user.EmployeePhone;
           // user.EmployeeEmail = dto.EmployeeEmail ?? user.EmployeeEmail;
            user.EmployeeCity = dto.EmployeeCity ?? user.EmployeeCity;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }
  
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return false;

            _userRepository.DeleteUser(user);
            return await _userRepository.SaveChangesAsync();
        }

    }

}

