using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Settings";
        return View();
    }
}