using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<Table>> GetTablesByBranchIdAsync(int branchId)
        {
            return await _tableRepository.GetTablesByBranchIdAsync(branchId);
        }

        public async Task<Table?> GetTableByIdAsync(int tableId)
        {
            return await _tableRepository.GetTableByIdAsync(tableId);
        }

        public async Task AddTableAsync(Table table)
        {
            await _tableRepository.AddTableAsync(table);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            await _tableRepository.DeleteTableAsync(tableId);
        }
    }
}
