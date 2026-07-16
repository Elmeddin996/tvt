using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SliderRepository : GenericRepository<Slider>, ISliderRepository
{
    public SliderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Slider>> GetActiveSlidersAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Where(s => s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();
    }
}
