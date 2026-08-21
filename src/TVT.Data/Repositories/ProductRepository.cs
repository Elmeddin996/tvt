using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Common.Filters;
using TVT.Core.Common.Pagination;
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

    public async Task<PagedResult<Product>> SearchAsync(
    string search,
    int? categoryId,
    PagedRequest request)
    {
        search = search.Trim().ToLower();

        var query = DbSet
            .AsNoTracking()
            .Include(x => x.ProductImages)
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Where(x =>
                x.IsActive &&
                (
                    x.NameAz.ToLower().Contains(search) ||
                    x.Code.ToLower().Contains(search) ||
                    x.Model.ToLower().Contains(search)
                ));

        if (categoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == categoryId.Value);
        }

        var totalCount = await query.CountAsync();

        var products = await query
            .OrderBy(x => x.NameAz)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Product>
        {
            Items = products,
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<PagedResult<Product>> GetByCategoryIdsAsync(
    IReadOnlyCollection<int> categoryIds,
    ProductFilterRequest filter,
    PagedRequest request)
    {
        var query = DbSet
            .AsNoTracking()
            .Include(x => x.ProductImages)
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .Where(x =>
                x.IsActive &&
                !x.IsDeleted &&
                categoryIds.Contains(x.CategoryId));

        // Brand filter
        if (filter.BrandIds.Any())
        {
            query = query.Where(x =>
                filter.BrandIds.Contains(x.BrandId));
        }

        // Minimum price filter
        if (filter.MinPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= filter.MinPrice.Value);
        }

        // Maximum price filter
        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= filter.MaxPrice.Value);
        }

        // Stock filter
        if (filter.InStock.HasValue)
        {
            if (filter.InStock.Value)
            {
                query = query.Where(x =>
                    x.StockQuantity > 0);
            }
            else
            {
                query = query.Where(x =>
                    x.StockQuantity <= 0);
            }
        }

        var totalCount = await query.CountAsync();

        var products = await query
            .OrderBy(x => x.NameAz)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Product>
        {
            Items = products,
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<ProductFilterOptions> GetFilterOptionsAsync(
    IReadOnlyCollection<int> categoryIds)
    {
        var query = DbSet
            .AsNoTracking()
            .Include(x => x.Brand)
            .Where(x =>
                x.IsActive &&
                !x.IsDeleted &&
                categoryIds.Contains(x.CategoryId));

        var brands = await query
            .GroupBy(x => new
            {
                x.BrandId,
                x.Brand.NameAz
            })
            .Select(x => new ProductFilterBrand
            {
                BrandId = x.Key.BrandId,
                Name = x.Key.NameAz,
                ProductCount = x.Count()
            })
            .OrderBy(x => x.Name)
            .ToListAsync();

        var minPrice = await query
            .Select(x => (decimal?)x.Price)
            .MinAsync() ?? 0;

        var maxPrice = await query
            .Select(x => (decimal?)x.Price)
            .MaxAsync() ?? 0;

        var inStockCount = await query
            .CountAsync(x => x.StockQuantity > 0);

        var outOfStockCount = await query
            .CountAsync(x => x.StockQuantity <= 0);

        return new ProductFilterOptions
        {
            Brands = brands,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockCount = inStockCount,
            OutOfStockCount = outOfStockCount
        };
    }
}
