using TVT.Business.DTOs.Categories;
using TVT.Business.DTOs.Products;
using TVT.Core.Common.Pagination;

namespace TVT.Web.ViewModels.Search;

public class SearchViewModel
{
    public string? Search { get; set; }

    public int? CategoryId { get; set; }

    public List<CategoryListDto> Categories { get; set; } = new();

    public PagedResult<ProductListDto> Products { get; set; } = new();
}
