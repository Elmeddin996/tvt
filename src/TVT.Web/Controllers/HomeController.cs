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

    public HomeController(
    ILogger<HomeController> logger,
    ISliderService sliderService,
    IMiniSliderService miniSliderService)
    {
        _logger = logger;
        _sliderService = sliderService;
        _miniSliderService = miniSliderService;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetActiveSlidersAsync();

        var miniSliders = await _miniSliderService.GetActiveAsync();

        var model = new HomeViewModel
        {
            Sliders = sliders,
            MiniSliders = miniSliders
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
