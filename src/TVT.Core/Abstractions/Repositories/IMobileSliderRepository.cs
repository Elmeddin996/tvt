using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IMobileSliderRepository : IGenericRepository<MobileSlider>
{
    Task<List<MobileSlider>> GetActiveAsync();

    Task<PagedResult<MobileSlider>> GetPagedAsync(
        PagedRequest request);
}
