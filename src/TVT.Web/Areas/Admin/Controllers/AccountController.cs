using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{

    private readonly IConfiguration _configuration;

    public AccountController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        string username,
        string password)
    {
        var adminUsername =  _configuration["AdminCredentials:Username"];

        var adminPassword =  _configuration["AdminCredentials:Password"];

        if (username == adminUsername && password == adminPassword)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(
                claims,
                "AdminCookie");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                "AdminCookie",
                principal);

            return RedirectToAction(
                "Index",
                "Dashboard",
                new { area = "Admin" });
        }

        ViewBag.Error = "İstifadəçi adı və ya şifrə yanlışdır.";

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("AdminCookie");

        return RedirectToAction(
            "Login",
            "Account",
            new { area = "Admin" });
    }
}
