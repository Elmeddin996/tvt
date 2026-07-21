using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CustomerController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Customers";
        return View();
    }
}