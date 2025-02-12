using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // ✅ GET /api/menu/restaurant/{id}
        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetMenuItemsByRestaurant(int id)
        {
            var menuItems = await _menuService.GetMenuItemsByRestaurantIdAsync(id);
            return Ok(menuItems);
        }

        // ✅ POST /api/menu
        [HttpPost]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDto menuItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _menuService.AddMenuItemAsync(menuItemDto);
            return Ok(new { message = "Menu item added successfully." });
        }
    }
}

