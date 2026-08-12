using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Products;

namespace TVT.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;

    public CartController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Products([FromQuery] int[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            return Json(Array.Empty<ProductDetailDto>());
        }

        var products = new List<ProductDetailDto>();

        foreach (var id in ids.Distinct())
        {
            var product = await _productService.GetByIdAsync(id);

            if (product != null)
            {
                products.Add(product);
            }
        }

        return Json(products);
    }
}
