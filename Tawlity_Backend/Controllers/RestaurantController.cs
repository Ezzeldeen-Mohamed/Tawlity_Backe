using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("with-menu/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _service.GetByIdWithMenuAsync(id);
            return restaurant != null ? Ok(restaurant) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurant = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, dto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var results = await _service.SearchAsync(query);
            return Ok(results);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearby([FromQuery] double lat, [FromQuery] double lon, [FromQuery] double radius)
        {
            var results = await _service.GetNearbyAsync(lat, lon, radius);
            return Ok(results);
        }
    }
}
