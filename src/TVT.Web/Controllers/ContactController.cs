using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;

namespace TVT.Web.Controllers;

public class ContactController : Controller
{
    private readonly ISettingService _settingService;

    public ContactController(ISettingService settingService)
    {
        _settingService = settingService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var setting = await _settingService.GetAsync();

        if (setting == null)
            return NotFound();

        ViewData["Title"] = "Əlaqə";

        return View(setting);
    }
}
