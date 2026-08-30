using Microsoft.Extensions.Localization;
using TVT.Web.Resources;

namespace TVT.Web.Services;

public class LocService
{
    private readonly IStringLocalizer _localizer;

    public LocService(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResource);

        var assemblyName = new System.Reflection.AssemblyName(
            type.Assembly.FullName);

        _localizer = factory.Create(
            "SharedResource",
            assemblyName.Name!);
    }

    public LocalizedString GetLocalizedHtmlString(string key)
    {
        return _localizer[key];
    }
}
