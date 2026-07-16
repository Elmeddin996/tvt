using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IPageRepository : IGenericRepository<Page>
{
    Task<Page?> GetBySlugAsync(string slug);
}
