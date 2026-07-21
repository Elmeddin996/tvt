using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Orders";
        return View();
    }
}