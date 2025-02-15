using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Dtos;

[Route("api/tables")]
[ApiController]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    // 🔹 GET: /api/tables/branch/{branchId}

    // 🔹 GET: /api/tables/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTableById(int id)
    {
        var table = await _tableService.GetTableByIdAsync(id);
        if (table == null) return NotFound();

        return Ok(table);
    }

    // 🔹 POST: /api/tables
    [HttpPost]
    public async Task<IActionResult> AddTable([FromBody] CreateTableDto tableDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _tableService.AddTableAsync(tableDto);
        return Ok(new { message = "Table added successfully." });
    }

    // 🔹 DELETE: /api/tables/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        var deleted = await _tableService.DeleteTableAsync(id);
        if (!deleted) return NotFound();

        return Ok(new { message = "Table deleted successfully." });
    }
}
