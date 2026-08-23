using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IMiniSliderRepository : IGenericRepository<MiniSlider>
{
    Task<List<MiniSlider>> GetActiveAsync();

    Task<PagedResult<MiniSlider>> GetPagedAsync(PagedRequest request);
}
