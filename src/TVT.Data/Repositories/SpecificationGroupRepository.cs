using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SpecificationGroupRepository
    : GenericRepository<SpecificationGroup>,
      ISpecificationGroupRepository
{
    public SpecificationGroupRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public override async Task<List<SpecificationGroup>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public override async Task<SpecificationGroup?> GetByIdAsync(int id)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
}
