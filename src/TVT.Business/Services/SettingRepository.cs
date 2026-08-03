using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SettingRepository
    : GenericRepository<Setting>,
      ISettingRepository
{
    public SettingRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<Setting?> GetSettingAsync()
    {
        return await DbSet
            .FirstOrDefaultAsync(x => !x.IsDeleted);
    }
}
