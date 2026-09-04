using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : BaseAdminController
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Blog";
        return View();
    }
}
