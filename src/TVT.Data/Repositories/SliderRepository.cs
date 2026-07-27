using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SliderRepository : GenericRepository<Slider>, ISliderRepository
{
    public SliderRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Slider>> GetActiveSlidersAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task<PagedResult<Slider>> GetPagedAsync(PagedRequest request)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.TitleAz!.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.TitleAz)
            .Skip((request.Page - 1) * 20)
            .Take(20)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResult<Slider>
        {
            Items = items,
            CurrentPage = request.Page,
            PageSize = 20,
            TotalCount = totalCount
        };
    }
}
