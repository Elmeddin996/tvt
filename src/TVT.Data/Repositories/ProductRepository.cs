using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Product>> GetFeaturedProductsAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Include(x => x.ProductImages)
            .Where(x =>
                x.IsFeatured &&
                x.IsActive &&
                !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<Product>> GetNewProductsAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Include(x => x.ProductImages)
            .Where(x =>
                x.IsNew &&
                x.IsActive &&
                !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Include(x => x.ProductImages)
            .Include(x => x.ProductSpecifications)
                .ThenInclude(x => x.Specification)
                    .ThenInclude(x => x.SpecificationGroup)
            .FirstOrDefaultAsync(x =>
                !x.IsDeleted &&
                (
                    x.SlugAz == slug ||
                    x.SlugEn == slug ||
                    x.SlugRu == slug
                ));
    }

    public override async Task<List<Product>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Include(x => x.ProductImages)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await DbSet
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Include(x => x.ProductImages)
            .Include(x => x.ProductSpecifications)
                .ThenInclude(x => x.Specification)
                    .ThenInclude(x => x.SpecificationGroup)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
}
