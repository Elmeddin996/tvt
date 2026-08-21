using TVT.Business.DTOs.Products;
using TVT.Core.Common.Pagination;

namespace TVT.Business.DTOs.Categories;

public class CategoryPageDto
{
    public CategoryDetailDto Category { get; set; } = null!;

    public List<CategoryListDto> ChildCategories { get; set; } = new();

    public PagedResult<ProductListDto> Products { get; set; } = new();
}
