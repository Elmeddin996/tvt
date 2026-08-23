using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class MiniSliderRepository
    : GenericRepository<MiniSlider>, IMiniSliderRepository
{
    public MiniSliderRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<MiniSlider>> GetActiveAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ToListAsync();
    }

    public async Task<PagedResult<MiniSlider>> GetPagedAsync(
        PagedRequest request)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Id)
            .Skip((request.Page - 1) * 20)
            .Take(20)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResult<MiniSlider>
        {
            Items = items,
            CurrentPage = request.Page,
            PageSize = 20,
            TotalCount = totalCount
        };
    }
}
