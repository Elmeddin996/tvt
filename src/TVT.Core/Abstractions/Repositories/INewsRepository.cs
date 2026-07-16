using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface INewsRepository : IGenericRepository<News>
{
    Task<News?> GetBySlugAsync(string slug);
    Task<List<News>> GetLatestNewsAsync(int count);
    Task<List<News>> GetPublishedNewsAsync();
}
