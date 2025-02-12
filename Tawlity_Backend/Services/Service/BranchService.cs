using AutoMapper;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Dtos;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public BranchService(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BranchDto>> GetBranchesByRestaurantIdAsync(int restaurantId)
    {
        var branches = await _branchRepository.GetBranchesByRestaurantIdAsync(restaurantId);
        return _mapper.Map<IEnumerable<BranchDto>>(branches);
    }

    public async Task<BranchDto?> GetBranchByIdAsync(int branchId)
    {
        var branch = await _branchRepository.GetBranchByIdAsync(branchId);
        return branch == null ? null : _mapper.Map<BranchDto>(branch);
    }

    public async Task AddBranchAsync(CreateBranchDto branchDto)
    {
        var branch = _mapper.Map<Branch>(branchDto);
        await _branchRepository.AddBranchAsync(branch);
    }

    public async Task DeleteBranchAsync(int branchId)
    {
        await _branchRepository.DeleteBranchAsync(branchId);
    }
}
