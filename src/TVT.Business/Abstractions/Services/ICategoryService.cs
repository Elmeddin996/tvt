using TVT.Core.Common.Pagination;
using TVT.Business.DTOs.Categories;

namespace TVT.Business.Abstractions.Services;

public interface ICategoryService
{
    Task<List<CategoryListDto>> GetAllAsync();
    Task<CategoryDetailDto?> GetByIdAsync(int id);
    Task<CategoryDetailDto?> GetBySlugAsync(
    string slug,
    string culture);
    Task<int> CreateAsync(CreateCategoryDto dto);
    Task UpdateAsync(UpdateCategoryDto dto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);

    Task<List<CategoryListDto>> GetParentCategoriesAsync(int? excludeCategoryId = null);
    Task<PagedResult<CategoryListDto>> GetPagedAsync(PagedRequest request);

    Task<List<int>> GetDescendantCategoryIdsAsync(int categoryId);
    Task<List<CategoryListDto>> GetSubCategoriesAsync(int parentId);
}
