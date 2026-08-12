using TVT.Business.DTOs.Products;
using TVT.Core.Common.Pagination;

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
    Task<PagedResult<ProductListDto>> SearchAsync(
     string search,
     int? categoryId,
     PagedRequest request);
}
