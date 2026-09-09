using AutoMapper;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Branches;
using TVT.Core.Abstractions.UnitOfWork;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class BranchService : IBranchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BranchService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<BranchListDto>> GetPagedAsync(
        PagedRequest request)
    {
        var result = await _unitOfWork.Branches.GetPagedAsync(request);

        return new PagedResult<BranchListDto>
        {
            Items = _mapper.Map<List<BranchListDto>>(result.Items),
            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<List<BranchListDto>> GetAllActiveAsync()
    {
        var branches = await _unitOfWork.Branches.GetAllActiveAsync();

        return _mapper.Map<List<BranchListDto>>(branches);
    }

    public async Task<BranchListDto?> GetByIdAsync(int id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);

        if (branch == null)
            return null;

        return _mapper.Map<BranchListDto>(branch);
    }

    public async Task<int> CreateAsync(CreateBranchDto dto)
    {
        var branch = _mapper.Map<Branch>(dto);

        await _unitOfWork.Branches.AddAsync(branch);
        await _unitOfWork.SaveChangesAsync();

        return branch.Id;
    }

    public async Task<bool> UpdateAsync(UpdateBranchDto dto)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(dto.Id);

        if (branch == null)
            return false;

        _mapper.Map(dto, branch);

        branch.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.Branches.UpdateAsync(branch);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);

        if (branch == null)
            return false;

        branch.IsDeleted = true;
        branch.IsActive = false;
        branch.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.Branches.UpdateAsync(branch);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
