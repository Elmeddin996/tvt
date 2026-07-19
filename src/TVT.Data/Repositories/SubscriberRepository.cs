using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class SubscriberRepository : GenericRepository<Subscriber>, ISubscriberRepository
{
    public SubscriberRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<Subscriber?> GetByEmailAsync(string email)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(s => s.Email == email);
    }
}
