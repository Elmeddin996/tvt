using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Category>> GetMainCategoriesAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.ParentId == null && c.IsActive)
            .ToListAsync();
    }

    public async Task<List<Category>> GetSubCategoriesAsync(int parentId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.ParentId == parentId && c.IsActive)
            .ToListAsync();
    }

    public async Task<Category?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IsActive && (c.SlugAz == slug || c.SlugEn == slug || c.SlugRu == slug));
    }
}
