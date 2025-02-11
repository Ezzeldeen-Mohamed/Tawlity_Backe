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
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllRestaurantsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _service.GetRestaurantByIdAsync(id);
            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> Create([FromBody] RestaurantDto dto)
        {
            await _service.AddRestaurantAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Name }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> Update(int id, [FromBody] RestaurantDto dto)
        {
            await _service.UpdateRestaurantAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRestaurantAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
            => Ok(await _service.SearchRestaurantsAsync(query));

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearby([FromQuery] double lat, [FromQuery] double lon, [FromQuery] double radius)
            => Ok(await _service.GetNearbyRestaurantsAsync(lat, lon, radius));
    }
}
