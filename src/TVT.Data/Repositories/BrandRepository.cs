using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
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
}
