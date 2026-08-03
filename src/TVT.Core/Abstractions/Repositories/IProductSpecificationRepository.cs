using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IProductSpecificationRepository : IGenericRepository<ProductSpecification>
{
    Task<List<ProductSpecification>> GetByProductIdAsync(int productId);

    Task DeleteByProductIdAsync(int productId);
}
