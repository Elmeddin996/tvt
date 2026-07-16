using Microsoft.EntityFrameworkCore;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;
using TVT.Data.Context;

namespace TVT.Data.Repositories;

public class NewsRepository : GenericRepository<News>, INewsRepository
{
    public NewsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<News?> GetBySlugAsync(string slug)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(n =>
                n.SlugAz == slug ||
                n.SlugEn == slug ||
                n.SlugRu == slug);
    }

    public async Task<List<News>> GetLatestNewsAsync(int count)
    {
        return await DbSet
            .AsNoTracking()
            .OrderByDescending(n => n.PublishedDate)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<News>> GetPublishedNewsAsync()
    {
        return await DbSet
            .AsNoTracking()
            .OrderByDescending(n => n.PublishedDate)
            .ToListAsync();
    }
}
