using TVT.Business.DTOs.Brands;
using TVT.Core.Common.Pagination;

namespace TVT.Business.Abstractions.Services;

public interface IBrandService
{
    Task<BrandDetailDto?> GetByIdAsync(int id);
    Task<BrandDetailDto?> GetBySlugAsync(string slug);
    Task<int> CreateAsync(CreateBrandDto dto);
    Task UpdateAsync(UpdateBrandDto dto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<PagedResult<BrandListDto>> GetPagedAsync(PagedRequest request);
}
