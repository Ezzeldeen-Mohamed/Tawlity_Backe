using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants() =>
            Ok(await _service.GetAllRestaurantsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _service.GetRestaurantByIdAsync(id);
            return restaurant == null ? NotFound() : Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto dto)
        {
            var newRestaurant = await _service.CreateRestaurantAsync(dto);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = newRestaurant.Id }, newRestaurant);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDto dto)
        {
            return await _service.UpdateRestaurantAsync(id, dto) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            return await _service.DeleteRestaurantAsync(id) ? NoContent() : NotFound();
        }
    }
}
