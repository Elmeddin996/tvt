using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Sliders";
        return View();
    }
}