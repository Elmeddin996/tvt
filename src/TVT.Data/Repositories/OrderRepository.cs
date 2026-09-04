using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<PagedResult<Order>> GetPagedAsync(
        PagedRequest request)
    {
        var query = DbSet
            .AsNoTracking()
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Where(x => !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            query = query.Where(x =>
                x.CustomerName.Contains(search) ||
                x.Phone.Contains(search) ||
                (x.Email != null && x.Email.Contains(search)) ||
                x.Id.ToString().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var orders = await query
            .OrderByDescending(x => x.CreatedDate)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Order>
        {
            Items = orders,
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Order?> GetByIdWithItemsAsync(int id)
    {
        return await DbSet
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);
    }
}
