using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface ISpecificationRepository : IGenericRepository<Specification>
{
    Task<List<Specification>> GetForProductAsync(int productId);
}
