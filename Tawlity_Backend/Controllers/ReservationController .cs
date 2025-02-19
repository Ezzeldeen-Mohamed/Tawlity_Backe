using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Tawlity_Backend.SomeThingsWeWillUseInTheFuther;

[Route("api/[Controller]")]
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
   // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        return Ok(reservations);
    }

    // 🔹 GET: /api/reservations/user (Get logged-in user's reservations)
    [HttpGet("user")]
   // [Authorize]
    public async Task<IActionResult> GetReservationsByUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        if (userId == 0) return Unauthorized("Invalid user token");

        var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
        return Ok(reservations);
    }

    // 🔹 GET: /api/reservations/{id} (Admin/User who made the reservation)
    [HttpGet("{id}")]
   // [Authorize]
    public async Task<IActionResult> GetReservationById(int id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound("Reservation not found.");

        return Ok(reservation);
    }

    // 🔹 POST: /api/reservations (Create Reservation)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0) return Unauthorized("Invalid user token");

             _reservationService.AddReservationAsync(userId, reservationDto);
            return Ok(new { message = "Reservation created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 🔹 PUT: /api/reservations/{id} (Update Reservation)
    [HttpPut("{id}")]
   // [Authorize]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto updatedReservationDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _reservationService.UpdateReservationAsync(id, updatedReservationDto);
        if (!updated) return NotFound("Reservation not found.");

        return Ok(new { message = "Reservation updated successfully" });
    }

    // 🔹 DELETE: /api/reservations/{id} (Cancel/Delete Reservation)
    [HttpDelete("{id}")]
    //[Authorize]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var deleted = await _reservationService.DeleteReservationAsync(id);
        if (!deleted) return NotFound("Reservation not found.");

        return Ok(new { message = "Reservation deleted successfully" });
    }
}
