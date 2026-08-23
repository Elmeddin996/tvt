using TVT.Business.DTOs.MiniSliders;
using TVT.Core.Common.Pagination;

namespace TVT.Business.Abstractions.Services;

public interface IMiniSliderService
{
    Task<PagedResult<MiniSliderListDto>> GetPagedAsync(
        PagedRequest request);

    Task<IReadOnlyList<MiniSliderListDto>> GetActiveAsync();

    Task<MiniSliderDetailDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateMiniSliderDto dto);

    Task UpdateAsync(UpdateMiniSliderDto dto);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
