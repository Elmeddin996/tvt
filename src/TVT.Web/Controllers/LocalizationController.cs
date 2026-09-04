using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;

namespace TVT.Web.Controllers;

public class LocalizationController : Controller
{
    private readonly ICategoryService _categoryService;

    public LocalizationController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> SetLanguage(
        string culture,
        string? returnUrl = null)
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

        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            var uri = new Uri(
                $"{Request.Scheme}://{Request.Host}{returnUrl}");

            var path = uri.AbsolutePath;

            var segments = path
                .Split(
                    '/',
                    StringSplitOptions.RemoveEmptyEntries);

            // Kateqoriya URL-i:
            // /az/slug
            if (segments.Length == 2 &&
                (segments[0] == "az" ||
                 segments[0] == "en" ||
                 segments[0] == "ru"))
            {
                var currentCulture = segments[0];
                var currentSlug = segments[1];

                var currentCultureCode = currentCulture switch
                {
                    "az" => "az-AZ",
                    "en" => "en-US",
                    "ru" => "ru-RU",
                    _ => null
                };

                if (currentCultureCode != null)
                {
                    var category =
                        await _categoryService.GetBySlugAsync(
                            currentSlug,
                            currentCultureCode);

                    if (category != null)
                    {
                        var newCulture = culture switch
                        {
                            "az-AZ" => "az",
                            "en-US" => "en",
                            "ru-RU" => "ru",
                            _ => "az"
                        };

                        var newSlug = newCulture switch
                        {
                            "az" => category.SlugAz,
                            "en" => category.SlugEn,
                            "ru" => category.SlugRu,
                            _ => category.SlugAz
                        };

                        if (!string.IsNullOrWhiteSpace(newSlug))
                        {
                            return LocalRedirect(
                                $"/{newCulture}/{newSlug}");
                        }
                    }
                }
            }
        }

        return LocalRedirect(
            string.IsNullOrWhiteSpace(returnUrl)
                ? "/"
                : returnUrl);
    }
}
