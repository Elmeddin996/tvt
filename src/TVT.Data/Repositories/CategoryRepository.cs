using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Pagination;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Category>> GetAllActiveAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync();
    }

    public async Task<List<Category>> GetSubCategoriesAsync(int parentId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.ParentId == parentId && c.IsActive)
            .ToListAsync();
    }

    public async Task<bool> HasSubCategoriesAsync(int categoryId)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(x => x.ParentId == categoryId && x.IsActive);
    }

    public async Task<Category?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IsActive && (c.SlugAz == slug || c.SlugEn == slug || c.SlugRu == slug));
    }

    public override async Task<List<Category>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Parent)
            .OrderBy(x => x.NameAz)
            .ToListAsync();
    }

    public async Task<PagedResult<Category>> GetPagedAsync(PagedRequest request)
    {
        var query = DbSet
            .AsNoTracking()
            .Include(x => x.Parent)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.NameAz.ToLower().Contains(search) ||
                x.NameEn.ToLower().Contains(search) ||
                x.NameRu.ToLower().Contains(search) ||
                x.SlugAz.ToLower().Contains(search) ||
                x.SlugEn.ToLower().Contains(search) ||
                x.SlugRu.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.NameAz)
            .Skip((request.Page - 1) * 20)
            .Take(20)
            .ToListAsync();

        return new PagedResult<Category>
        {
            Items = items,
            CurrentPage = request.Page,
            PageSize = 20,
            TotalCount = totalCount
        };
    }
}
