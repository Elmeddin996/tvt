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

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return Redirect("/");
        }

        var uri = new Uri(
            $"{Request.Scheme}://{Request.Host}{returnUrl}");

        // LocalPath Unicode simvolları decoded formada verir.
        // Məsələn:
        // /ru/%D0%9C%D0%BE%D0%BD%D0%B8%D1%82%D0%BE%D1%80%D1%8B
        // ->
        // /ru/Мониторы
        var path = uri.LocalPath;

        var segments = path
            .Split(
                '/',
                StringSplitOptions.RemoveEmptyEntries);

        /*
         * Kateqoriya URL-i:
         *
         * /az/slug
         * /en/slug
         * /ru/slug
         */
        if (segments.Length == 2 &&
            (segments[0].Equals("az", StringComparison.OrdinalIgnoreCase) ||
             segments[0].Equals("en", StringComparison.OrdinalIgnoreCase) ||
             segments[0].Equals("ru", StringComparison.OrdinalIgnoreCase)))
        {
            var currentCulture =
                segments[0].ToLowerInvariant();

            var currentSlug =
                Uri.UnescapeDataString(segments[1]);

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
                        return Redirect(
                            $"/{newCulture}/{newSlug}");
                    }
                }
            }
        }

        return Redirect(returnUrl);
    }
}
