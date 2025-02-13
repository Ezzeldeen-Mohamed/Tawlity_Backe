using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity.Core.Enums;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Admin-only access
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // 🔹 GET: /api/admin/analytics (Get system-wide analytics)
        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics()
        {
            var analytics = await _adminService.GetAnalyticsAsync();
            return Ok(analytics);
        }

        // 🔹 POST: /api/admin/seed-roles (Seed default roles)
        [HttpPost("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var success = await _adminService.SeedRolesAsync();
            if (!success) return BadRequest("Roles already exist.");

            return Ok(new { message = "Roles seeded successfully." });
        }

        // 🔹 POST: /api/admin/seed-restaurants (Seed sample restaurants)
        [HttpPost("seed-restaurants")]
        public async Task<IActionResult> SeedRestaurants()
        {
            var success = await _adminService.SeedSampleRestaurantsAsync();
            if (!success) return BadRequest("Restaurants already exist.");

            return Ok(new { message = "Sample restaurants seeded successfully." });
        }
    }
}
