using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SettingRepository : GenericRepository<Setting>, ISettingRepository
{
    public SettingRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
