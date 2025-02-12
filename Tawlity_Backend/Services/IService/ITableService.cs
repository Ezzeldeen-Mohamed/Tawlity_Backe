
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetTablesByBranchIdAsync(int branchId);
        Task<Table?> GetTableByIdAsync(int tableId);
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(int tableId);
    }
}
