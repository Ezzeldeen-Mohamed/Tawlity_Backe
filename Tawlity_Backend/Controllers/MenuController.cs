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

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetMenuItemsByRestaurantId(int restaurantId)
        {
            var menuItems = await _menuService.GetMenuItemsByRestaurantIdAsync(restaurantId);
            return Ok(menuItems);
        }

        [HttpGet("Item/{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null) return NotFound();

            return Ok(menuItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMetemDto menuItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _menuService.AddMenuItemAsync(menuItemDto);
                return CreatedAtAction(nameof(GetMenuItemsByRestaurantId), new { restaurantId = menuItemDto.RestaurantId }, menuItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}

