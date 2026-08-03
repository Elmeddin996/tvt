using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.Settings;

namespace TVT.Web.Areas.Admin.ViewModels.Settings;

public class UpdateSettingViewModel
{
    public UpdateSettingDto Setting { get; set; } = new();

    public IFormFile? LogoFile { get; set; }
}
