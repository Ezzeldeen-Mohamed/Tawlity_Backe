
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetTablesByBranchIdAsync(int branchId);
        Task<TableDto?> GetTableByIdAsync(int tableId);
        Task AddTableAsync(CreateTableDto tableDto);
        Task<bool> DeleteTableAsync(int tableId);
    }
}
