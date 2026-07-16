using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class PageRepository : GenericRepository<Page>, IPageRepository
{
    public PageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Page?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.SlugAz == slug ||
                p.SlugEn == slug ||
                p.SlugRu == slug);
    }
}
