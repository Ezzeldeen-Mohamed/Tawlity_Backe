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
    private readonly ILogger<ReservationController> loger;
    public ReservationController(ILogger<ReservationController> loge, IReservationService reservationService)
    {
        loger = loge;
        _reservationService = reservationService;
    }

    // 🔹 GET: /api/reservations (Admin Only)
    [HttpGet]
    //[Authorize(Roles = "Admin")]
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
    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            bool success = await _reservationService.AddReservationAsync(reservationDto);
            if (success)
                return Ok(new { message = "Reservation created successfully" });
            else
                return BadRequest(new { message = "Failed to create reservation" });
        }
        catch (Exception ex)
        {
            loger.LogError(ex, "Error while creating reservation");

            if (ex.InnerException != null)
            {
                return BadRequest(new
                {
                    message = "Reservation error: " + ex.Message,
                    innerException = ex.InnerException?.Message, // ✅ عرض الخطأ الداخلي
                    stackTrace = ex.StackTrace // ✅ عرض مسار الخطأ
                });
            }

            return BadRequest(new { message = $"Reservation error: {ex.Message}" });
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
