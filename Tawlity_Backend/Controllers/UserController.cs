using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tawlity.Core.Enums;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 🔹 GET: /api/users (Admin-only)
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        // 🔹 GET: /api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound("User not found.");
        }

        // 🔹 PUT: /api/users/{id}
        //    [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var updated = await _userService.UpdateUserAsync(id, dto);
            return updated ? Ok("Success") : NotFound("User not found.");
        }

        // 🔹 DELETE: /api/users/{id} (Admin-only)
        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            return deleted ? NoContent() : NotFound("User not found.");
        }

        [HttpGet("user-info")]
        [Authorize(Roles ="Admin")]
        public IActionResult GetUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            {
                return Unauthorized(new { message = "Invalid user token." });
            }

            return Ok(new
            {
                UserId = userId,
                Role = userRole
            });
        }



        //[HttpGet("Profile")]
        //public async Task<IActionResult> GetUserProfile()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get logged-in user's ID
        //    //if (userId == null) return Unauthorized("User not authenticated.");

        //    var user = await _userService.GetUserByIdAsync(int.Parse(userId));
        //    if (user == null) return NotFound("User not found.");

        //    return Ok(new
        //    {
        //        user.EmployeeId,
        //        user.EmployeeName,
        //        user.EmployeeEmail,
        //        user.EmployeeCity
        //    });
        //}

    }
}
