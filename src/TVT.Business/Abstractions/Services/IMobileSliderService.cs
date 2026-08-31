using TVT.Business.DTOs.MobileSliders;
using TVT.Core.Common.Pagination;

namespace TVT.Business.Abstractions.Services;

public interface IMobileSliderService
{
    Task<PagedResult<MobileSliderListDto>> GetPagedAsync(
        PagedRequest request);

    Task<IReadOnlyList<MobileSliderListDto>> GetActiveAsync();

    Task<MobileSliderDetailDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateMobileSliderDto dto);

    Task UpdateAsync(UpdateMobileSliderDto dto);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
