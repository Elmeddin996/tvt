using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Web.Models;
using TVT.Web.ViewModels.Home;

namespace TVT.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ISliderService _sliderService;
    private readonly IMiniSliderService _miniSliderService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public HomeController(
    ILogger<HomeController> logger,
    ISliderService sliderService,
    IMiniSliderService miniSliderService,
    ICategoryService categoryService,
    IProductService productService)
    {
        _logger = logger;
        _sliderService = sliderService;
        _miniSliderService = miniSliderService;
        _categoryService = categoryService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetActiveSlidersAsync();

        var miniSliders = await _miniSliderService.GetActiveAsync();

        var categories = await _categoryService.GetAllAsync();

        var newProducts = await _productService.GetNewProductsAsync(9);

        var model = new HomeViewModel
        {
            Sliders = sliders,
            MiniSliders = miniSliders,
            Categories = categories,
            NewProducts = newProducts.Take(9).ToList()
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
        });
    }
}
