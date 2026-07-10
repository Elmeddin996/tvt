using TVT.Business.Abstractions.Services;
using TVT.Core.Abstractions.Repositories;
using TVT.Core.Entities;

namespace TVT.Business.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetFeaturedProductsAsync()
    {
        return await _repository.GetFeaturedProductsAsync();
    }

    public async Task<List<Product>> GetNewProductsAsync()
    {
        return await _repository.GetNewProductsAsync();
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _repository.GetBySlugAsync(slug);
    }
}
