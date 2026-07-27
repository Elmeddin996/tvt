using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface ISliderRepository : IGenericRepository<Slider>
{
    Task<PagedResult<Slider>> GetPagedAsync(PagedRequest request);

    Task<List<Slider>> GetActiveSlidersAsync();
}
