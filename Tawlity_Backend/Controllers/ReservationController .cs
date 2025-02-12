using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Models;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // 🔹 GET: /api/reservations (Admin Only)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        // 🔹 GET: /api/reservations/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetReservationsByUser(int userId)
        {
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
            return Ok(reservations);
        }

        // 🔹 POST: /api/reservations
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _reservationService.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationsByUser), new { userId = reservation.UserId }, reservation);
        }

        // 🔹 PUT: /api/reservations/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            var updated = await _reservationService.UpdateReservationAsync(id, updatedReservation);
            if (!updated) return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: /api/reservations/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var deleted = await _reservationService.DeleteReservationAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

