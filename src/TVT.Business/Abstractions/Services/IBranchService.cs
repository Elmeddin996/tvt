using TVT.Business.DTOs.Branches;
using TVT.Core.Common.Pagination;

namespace TVT.Business.Abstractions.Services;

public interface IBranchService
{
    Task<PagedResult<BranchListDto>> GetPagedAsync(PagedRequest request);

    Task<List<BranchListDto>> GetAllActiveAsync();

    Task<BranchListDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateBranchDto dto);

    Task<bool> UpdateAsync(UpdateBranchDto dto);

    Task<bool> DeleteAsync(int id);
}
