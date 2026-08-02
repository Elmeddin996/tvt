using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SpecificationRepository
    : GenericRepository<Specification>,
      ISpecificationRepository
{
    public SpecificationRepository(ApplicationDbContext context)
        : base(context)
    {
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
}
