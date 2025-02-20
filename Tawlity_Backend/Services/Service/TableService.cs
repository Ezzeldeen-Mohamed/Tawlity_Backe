using AutoMapper;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Dtos; // Import DTOs

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<TableDto?> GetTableByIdAsync(int tableId)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId);
        if (table == null) return null;

        // تحويل يدوي من Table إلى TableDto
        return new TableDto
        {
            Id = table.Id,
            Capacity = table.Capacity,
            ImageUrl = table.ImageUrl,
            RestaurantId = table.RestaurantId
        };
    }

    public async Task AddTableAsync(CreateTableDto tableDto)
    {
        // تحويل يدوي من CreateTableDto إلى Table
        var table = new Table
        {
            Capacity = tableDto.Capacity,
            ImageUrl = tableDto.ImageUrl,
            RestaurantId = tableDto.RestaurantId
        };

        await _tableRepository.AddTableAsync(table);
    }

    public async Task<bool> DeleteTableAsync(int tableId)
    {
        var existingTable = await _tableRepository.GetTableByIdAsync(tableId);
        if (existingTable == null) return false;

        await _tableRepository.DeleteTableAsync(tableId);
        return true;
    }
}
