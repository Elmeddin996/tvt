using TVT.Business.DTOs.Products;

namespace TVT.Business.Abstractions.Services;

public interface IProductService
{
    Task<List<ProductListDto>> GetAllAsync();
    Task<ProductDetailDto?> GetByIdAsync(int id);
    Task<ProductDetailDto?> GetBySlugAsync(string slug);
    Task<int> CreateAsync(CreateProductDto dto);
    Task UpdateAsync(UpdateProductDto dto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
