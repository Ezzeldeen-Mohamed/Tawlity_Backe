using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Dtos;
using System.Security.Claims;

[Route("api/reservations")]
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


    // 🔹 PUT: /api/reservations/{id}
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto updatedReservationDto)
    {
        var updated = await _reservationService.UpdateReservationAsync(id, updatedReservationDto);
        if (!updated) return NotFound();

        return Ok(new { message = "Reservation updated successfully" });
    }

    // 🔹 DELETE: /api/reservations/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var deleted = await _reservationService.DeleteReservationAsync(id);
        if (!deleted) return NotFound();

        return Ok(new { message = "Reservation deleted successfully" });
    }

    // 🔹 POST: /api/reservations
    [HttpPost]
    [Authorize] // User must be logged in
    public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Extract the User ID from the JWT token
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        if (userId == 0)
            return Unauthorized("Invalid user token");

        // Pass user ID to the service
        await _reservationService.AddReservationAsync(userId, reservationDto);
        return Ok(new { message = "Reservation created successfully" });
    }
}
