using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;

namespace TVT.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return NotFound();

        var product = await _productService.GetBySlugAsync(slug);

        if (product == null)
            return NotFound();

        return View(product);
    }
}
