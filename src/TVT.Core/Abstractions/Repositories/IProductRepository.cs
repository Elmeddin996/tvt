using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetFeaturedProductsAsync();
    Task<List<Product>> GetNewProductsAsync();
    Task<Product?> GetBySlugAsync(string slug);
}
