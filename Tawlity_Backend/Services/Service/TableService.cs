using AutoMapper;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Dtos; // Import DTOs

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public TableService(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }


    public async Task<TableDto?> GetTableByIdAsync(int tableId)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId);
        return table == null ? null : _mapper.Map<TableDto>(table);
    }

    public async Task AddTableAsync(CreateTableDto tableDto)
    {
        var table = _mapper.Map<Table>(tableDto);
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
