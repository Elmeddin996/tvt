using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IBrandRepository : IGenericRepository<Brand>
{
    Task<Brand?> GetBySlugAsync(string slug);
    Task<List<Brand>> GetActiveBrandsAsync();
}
