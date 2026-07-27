using TVT.Business.DTOs.Sliders;
using TVT.Core.Common.Pagination;

namespace TVT.Business.Abstractions.Services;

public interface ISliderService
{
    Task<PagedResult<SliderListDto>> GetPagedAsync(PagedRequest request);

    Task<SliderDetailDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateSliderDto dto);

    Task UpdateAsync(UpdateSliderDto dto);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
