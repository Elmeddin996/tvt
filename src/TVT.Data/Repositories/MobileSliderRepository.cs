using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class MobileSliderRepository
    : GenericRepository<MobileSlider>, IMobileSliderRepository
{
    public MobileSliderRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<MobileSlider>> GetActiveAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(x =>
                x.IsActive &&
                !x.IsDeleted)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<PagedResult<MobileSlider>> GetPagedAsync(
        PagedRequest request)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResult<MobileSlider>
        {
            Items = items,
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
