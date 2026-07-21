using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class NewsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "News";
        return View();
    }
}