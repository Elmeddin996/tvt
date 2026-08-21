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

    public HomeController(
        ILogger<HomeController> logger,
        ISliderService sliderService)
    {
        _logger = logger;
        _sliderService = sliderService;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetActiveSlidersAsync();

        var model = new HomeViewModel
        {
            Sliders = sliders
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
