using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<PagedResult<Order>> GetPagedAsync(
        PagedRequest request);

    Task<Order?> GetByIdWithItemsAsync(int id);
}
