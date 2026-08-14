using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Products;

namespace TVT.Web.Controllers;

public class WishlistController : Controller
{
    private readonly IProductService _productService;

    public WishlistController(IProductService productService)
    {
        _productService = productService;
    }


    // Wishlist səhifəsi
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    // Wishlist məhsullarını gətir
    [HttpPost]
    public async Task<IActionResult> GetItems(
        [FromBody] List<int> productIds)
    {
        if (productIds is null || productIds.Count == 0)
        {
            return Json(new List<ProductDetailDto>());
        }

        var products = new List<ProductDetailDto>();

        foreach (var productId in productIds.Distinct())
        {
            var product = await _productService.GetByIdAsync(productId);

            if (product is not null)
            {
                products.Add(product);
            }
        }

        return Json(products);
    }
}
