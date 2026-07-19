using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Areas.Admin.Controllers;

public class DashboardController : BaseAdminController
{
    public IActionResult Index()
    {
        return View();
    }
}
