using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TVT.Web.Controllers;

public class LocalizationController : Controller
{
    [HttpPost]
    public IActionResult SetLanguage(string culture, string? returnUrl = null)
    {
        var supportedCultures = new[]
        {
            "az-AZ",
            "en-US",
            "ru-RU"
        };

        if (!supportedCultures.Contains(culture))
        {
            culture = "az-AZ";
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(
                new RequestCulture(culture)));

        return LocalRedirect(
            string.IsNullOrWhiteSpace(returnUrl)
                ? "/"
                : returnUrl);
    }
}
