using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Core.Common.Filters;
using TVT.Core.Common.Pagination;
using TVT.Web.ViewModels.Category;

namespace TVT.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public CategoryController(
        ICategoryService categoryService,
        IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        string culture,
        string slug,
        int page = 1,
        int pageSize = 20,
        List<int>? brandIds = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool? inStock = null)
    {
        if (string.IsNullOrWhiteSpace(culture) ||
            string.IsNullOrWhiteSpace(slug))
            return NotFound();

        var cultureCode = culture.ToLowerInvariant() switch
        {
            "az" => "az-AZ",
            "en" => "en-US",
            "ru" => "ru-RU",
            _ => null
        };

        if (cultureCode == null)
            return NotFound();

        if (page < 1)
            page = 1;

        if (pageSize <= 0)
            pageSize = 20;

        var category =
            await _categoryService.GetBySlugAsync(
                slug,
                cultureCode);

        if (category is null)
            return NotFound();

        var childCategories =
            await _categoryService.GetSubCategoriesAsync(category.Id);

        var categoryIds =
            await _categoryService.GetDescendantCategoryIdsAsync(category.Id);

        // Selected product filters
        var filter = new ProductFilterRequest
        {
            BrandIds = brandIds ?? new List<int>(),
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStock = inStock
        };

        // Pagination
        var request = new PagedRequest
        {
            Page = page,
            PageSize = pageSize
        };

        // Filter options for the View
        var filterOptions =
            await _productService.GetFilterOptionsAsync(categoryIds);

        // Filtered products
        var products =
            await _productService.GetByCategoryIdsAsync(
                categoryIds,
                filter,
                request);

        var viewModel = new CategoryPageViewModel
        {
            Category = category,
            ChildCategories = childCategories,
            FilterOptions = filterOptions,
            Products = products
        };

        return View(viewModel);
    }
}
