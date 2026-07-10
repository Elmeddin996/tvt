using TVT.Core.Entities;

namespace TVT.Business.Abstractions.Services;

public interface IProductService
{
    Task<List<Product>> GetFeaturedProductsAsync();
    Task<List<Product>> GetNewProductsAsync();
    Task<Product?> GetBySlugAsync(string slug);
}
