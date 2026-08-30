using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Products;
using TVT.Core.Common.Pagination;
using TVT.Web.ViewModels.Search;

namespace TVT.Web.Controllers;

public class SearchController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public SearchController(
    IProductService productService,
    ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
    string? q,
    int? category_id,
    int page = 1)
    {
        var request = new PagedRequest
        {
            Page = page,
            PageSize = 20
        };

        var model = new SearchViewModel
        {
            Search = q,
            CategoryId = category_id,
            Categories = await _categoryService.GetAllAsync()
        };

        if (!string.IsNullOrWhiteSpace(q))
        {
            model.Products = await _productService.SearchAsync(
                q,
                category_id,
                request);
        }

        return View(model);
    }

    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> LiveSearch(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return PartialView("_LiveSearch", new List<ProductListDto>());
        }

        var request = new PagedRequest
        {
            Page = 1,
            PageSize = 5
        };

        var result = await _productService.SearchAsync(
            keyword,
            null,
            request);

        return PartialView("_LiveSearch", result.Items);
    }
}
