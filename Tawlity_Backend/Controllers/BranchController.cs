using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Models;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // 🔹 GET: /api/branches/restaurant/{id}
        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetBranchesByRestaurant(int id)
        {
            var branches = await _branchService.GetBranchesByRestaurantIdAsync(id);
            return Ok(branches);
        }

        // 🔹 POST: /api/branches
        [HttpPost]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> AddBranch([FromBody] Branch branch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _branchService.AddBranchAsync(branch);
            return CreatedAtAction(nameof(GetBranchesByRestaurant), new { id = branch.RestaurantId }, branch);
        }

        // DELETE: /api/branches/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            await _branchService.DeleteBranchAsync(id);
            return NoContent();
        }

    }
}
