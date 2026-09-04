using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace TVT.Web.Services;

public class RouteDataRequestCultureProvider : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(
        HttpContext httpContext)
    {
        var culture = httpContext.Request.RouteValues["culture"]?.ToString();

        if (string.IsNullOrWhiteSpace(culture))
            return Task.FromResult<ProviderCultureResult?>(null);

        var cultureCode = culture.ToLowerInvariant() switch
        {
            "az" => "az-AZ",
            "en" => "en-US",
            "ru" => "ru-RU",
            _ => null
        };

        if (cultureCode == null)
            return Task.FromResult<ProviderCultureResult?>(null);

        return Task.FromResult<ProviderCultureResult?>(
            new ProviderCultureResult(cultureCode));
    }
}
