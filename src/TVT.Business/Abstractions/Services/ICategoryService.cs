using TVT.Core.Entities;

namespace TVT.Business.Abstractions.Services;

public interface ICategoryService
{
    Task<List<Category>> GetMainCategoriesAsync();
    Task<List<Category>> GetSubCategoriesAsync(int parentId);
    Task<Category?> GetBySlugAsync(string slug);
}
