using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IBrandRepository : IGenericRepository<Brand>
{
    Task<Brand?> GetBySlugAsync(string slug);
    Task<List<Brand>> GetActiveBrandsAsync();
    Task<PagedResult<Brand>> GetPagedAsync(PagedRequest request);
}
