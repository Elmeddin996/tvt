using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ContactController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Contacts";
        return View();
    }
}