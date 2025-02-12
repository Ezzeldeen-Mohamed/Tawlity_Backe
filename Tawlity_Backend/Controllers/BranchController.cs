using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

[ApiController]
[Route("api/branches")]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    // ✅ GET /api/branches/restaurant/{restaurantId}
    [HttpGet("restaurant/{restaurantId}")]
    public async Task<IActionResult> GetBranchesByRestaurant(int restaurantId)
    {
        var branches = await _branchService.GetBranchesByRestaurantIdAsync(restaurantId);
        return Ok(branches);
    }

    // ✅ GET /api/branches/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(int id)
    {
        var branch = await _branchService.GetBranchByIdAsync(id);
        if (branch == null) return NotFound();
        return Ok(branch);
    }

    // ✅ POST /api/branches
    [HttpPost]
    public async Task<IActionResult> AddBranch([FromBody] CreateBranchDto branchDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _branchService.AddBranchAsync(branchDto);
        return CreatedAtAction(nameof(GetBranchById), new { id = branchDto.RestaurantId }, branchDto);
    }

    // ✅ DELETE /api/branches/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBranch(int id)
    {
        await _branchService.DeleteBranchAsync(id);
        return NoContent();
    }
}
