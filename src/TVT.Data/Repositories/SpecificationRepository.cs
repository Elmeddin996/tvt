using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SpecificationRepository
    : GenericRepository<Specification>,
      ISpecificationRepository
{
    private readonly ApplicationDbContext _context;
    public SpecificationRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<List<Specification>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.SpecificationGroup)
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.SpecificationGroup.DisplayOrder)
            .ThenBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public override async Task<Specification?> GetByIdAsync(int id)
    {
        return await DbSet
            .Include(x => x.SpecificationGroup)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task<List<Specification>> GetForProductAsync(int productId)
    {
        var categoryId = await _context.Products
            .Where(x => x.Id == productId && !x.IsDeleted)
            .Select(x => x.CategoryId)
            .FirstAsync();

        return await DbSet
     .AsNoTracking()
     .Include(x => x.SpecificationGroup)
         .ThenInclude(x => x.CategorySpecificationGroups)
     .Include(x => x.ProductSpecifications
         .Where(x => x.ProductId == productId && !x.IsDeleted))
     .Where(x =>
         !x.IsDeleted &&
         x.SpecificationGroup.CategorySpecificationGroups
             .Any(c => c.CategoryId == categoryId))
     .OrderBy(x => x.SpecificationGroup.DisplayOrder)
     .ThenBy(x => x.DisplayOrder)
     .ToListAsync();
    }
}
