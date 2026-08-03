using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class ProductSpecificationRepository
    : GenericRepository<ProductSpecification>,
      IProductSpecificationRepository
{
    public ProductSpecificationRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<ProductSpecification>> GetByProductIdAsync(int productId)
    {
        return await DbSet
            .Where(x => x.ProductId == productId && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task DeleteByProductIdAsync(int productId)
    {
        var specifications = await DbSet
            .Where(x => x.ProductId == productId && !x.IsDeleted)
            .ToListAsync();

        DbSet.RemoveRange(specifications);
    }
}
