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
    private readonly IMobileSliderService _mobileSliderService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly ISettingService _settingService;

    public HomeController(
        ILogger<HomeController> logger,
        ISliderService sliderService,
        IMiniSliderService miniSliderService,
        IMobileSliderService mobileSliderService,
        ICategoryService categoryService,
        IProductService productService,
        ISettingService settingService)
    {
        _logger = logger;
        _sliderService = sliderService;
        _miniSliderService = miniSliderService;
        _mobileSliderService = mobileSliderService;
        _categoryService = categoryService;
        _productService = productService;
        _settingService = settingService;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetActiveSlidersAsync();

        var miniSliders = await _miniSliderService.GetActiveAsync();

        var mobileSliders = await _mobileSliderService.GetActiveAsync();

        var categories = await _categoryService.GetAllAsync();

        var newProducts = await _productService.GetNewProductsAsync(9);

        var setting = await _settingService.GetAsync();

        var model = new HomeViewModel
        {
            Sliders = sliders,
            MiniSliders = miniSliders,
            MobileSliders = mobileSliders,
            Categories = categories,
            NewProducts = newProducts.Take(9).ToList(),

            YoutubeVideo1 = setting?.YoutubeVideo1,
            YoutubeVideo2 = setting?.YoutubeVideo2
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
