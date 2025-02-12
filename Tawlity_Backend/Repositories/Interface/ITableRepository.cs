using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetTablesByBranchIdAsync(int branchId);
        Task<Table?> GetTableByIdAsync(int tableId);
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(int tableId);
    }
}
