using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<ProductImage>> GetByProductIdAsync(int productId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => x.ProductId == productId && !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task<ProductImage?> GetMainImageAsync(int productId)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.ProductId == productId &&
                x.IsMain &&
                !x.IsDeleted);
    }

    public async Task<bool> ExistsMainImageAsync(int productId)
    {
        return await DbSet.AnyAsync(x =>
            x.ProductId == productId &&
            x.IsMain &&
            !x.IsDeleted);
    }
}
