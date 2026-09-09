using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IBranchRepository : IGenericRepository<Branch>
{
    Task<List<Branch>> GetAllActiveAsync();

    Task<PagedResult<Branch>> GetPagedAsync(PagedRequest request);

    Task<Branch?> GetByIdAsync(int id);
}
