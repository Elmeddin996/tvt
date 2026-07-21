using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class BrandController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Brands";
        return View();
    }
}