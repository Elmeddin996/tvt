using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
{
    public BrandRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Brand?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b =>
                b.SlugAz == slug ||
                b.SlugEn == slug ||
                b.SlugRu == slug);
    }

    public async Task<List<Brand>> GetActiveBrandsAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(b => b.IsActive)
            .ToListAsync();
    }

    public async Task<PagedResult<Brand>> GetPagedAsync(PagedRequest request)
    {
        var query = DbSet
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.NameAz.ToLower().Contains(search) ||
                x.NameEn.ToLower().Contains(search) ||
                x.NameRu.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.NameAz)
            .Skip((request.Page - 1) * 20)
            .Take(20)
            .ToListAsync();

        return new PagedResult<Brand>
        {
            Items = items,
            CurrentPage = request.Page,
            PageSize = 20,
            TotalCount = totalCount
        };
    }
}
