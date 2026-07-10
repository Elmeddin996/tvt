using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Category>> GetMainCategoriesAsync();
    Task<List<Category>> GetSubCategoriesAsync(int parentId);
    Task<Category?> GetBySlugAsync(string slug);
}
