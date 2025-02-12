using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Models;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        // 🔹 GET: /api/tables/branch/{branchId}
        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetTablesByBranch(int branchId)
        {
            var tables = await _tableService.GetTablesByBranchIdAsync(branchId);
            return Ok(tables);
        }

        // 🔹 POST: /api/tables
        [HttpPost]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> AddTable([FromBody] Table table)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _tableService.AddTableAsync(table);
            return CreatedAtAction(nameof(GetTablesByBranch), new { branchId = table.BranchId }, table);
        }

        // 🔹 DELETE: /api/tables/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RestaurantOwner")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            await _tableService.DeleteTableAsync(id);
            return NoContent();
        }
    }
}
