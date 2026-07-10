using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetFeaturedProductsAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(p => p.IsFeatured && p.IsActive)
            .ToListAsync();
    }

    public async Task<List<Product>> GetNewProductsAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(p => p.IsNew && p.IsActive)
            .ToListAsync();
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.SlugAz == slug || p.SlugEn == slug || p.SlugRu == slug);
    }
}
