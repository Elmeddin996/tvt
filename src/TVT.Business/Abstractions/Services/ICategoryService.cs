using TVT.Business.DTOs.Categories;

namespace TVT.Business.Abstractions.Services;

public interface ICategoryService
{
    Task<List<CategoryListDto>> GetAllAsync();
    Task<CategoryDetailDto?> GetByIdAsync(int id);
    Task<CategoryDetailDto?> GetBySlugAsync(string slug);
    Task<int> CreateAsync(CreateCategoryDto dto);
    Task UpdateAsync(UpdateCategoryDto dto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
