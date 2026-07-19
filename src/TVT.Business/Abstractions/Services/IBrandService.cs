using TVT.Business.DTOs.Brands;

namespace TVT.Business.Abstractions.Services;

public interface IBrandService
{
    Task<List<BrandListDto>> GetAllAsync();
    Task<BrandDetailDto?> GetByIdAsync(int id);
    Task<BrandDetailDto?> GetBySlugAsync(string slug);
    Task<int> CreateAsync(CreateBrandDto dto);
    Task UpdateAsync(UpdateBrandDto dto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}