using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IProductImageRepository : IGenericRepository<ProductImage>
{
    Task<List<ProductImage>> GetByProductIdAsync(int productId);

    Task<ProductImage?> GetMainImageAsync(int productId);

    Task<bool> ExistsMainImageAsync(int productId);
}
