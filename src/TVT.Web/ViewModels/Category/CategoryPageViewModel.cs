using TVT.Business.DTOs.Categories;
using TVT.Business.DTOs.Products;
using TVT.Core.Common.Pagination;

namespace TVT.Web.ViewModels.Category;

public class CategoryPageViewModel
{
    public CategoryDetailDto Category { get; set; } = null!;

    public List<CategoryListDto> ChildCategories { get; set; } = new();

    public ProductFilterOptionsDto FilterOptions { get; set; } = new();

    public PagedResult<ProductListDto> Products { get; set; } = new();
}
