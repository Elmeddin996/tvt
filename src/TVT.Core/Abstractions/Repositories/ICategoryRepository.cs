using TVT.Core.Common.Pagination;
using TVT.Core.Entities;

namespace TVT.Core.Abstractions.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Category>> GetAllActiveAsync();
    Task<List<Category>> GetSubCategoriesAsync(int parentId);
    Task<Category?> GetBySlugAsync(string slug, string culture);
    Task<bool> HasSubCategoriesAsync(int categoryId);

    Task<PagedResult<Category>> GetPagedAsync(PagedRequest request);
}
