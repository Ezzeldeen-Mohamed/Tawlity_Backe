using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface ITableRepository
    {
        Task<Table?> GetTableByIdAsync(int tableId);
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(int tableId);
    }
}
