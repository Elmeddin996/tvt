using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Settings;
using TVT.Web.Areas.Admin.ViewModels.Settings;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingsController : BaseAdminController
{
    private readonly ISettingService _settingService;
    private readonly IFileService _fileService;

    public SettingsController(
        ISettingService settingService,
        IFileService fileService)
    {
        _settingService = settingService;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var setting = await _settingService.GetAsync();

        if (setting == null)
            return NotFound();

        var model = new UpdateSettingViewModel
        {
            Setting = new UpdateSettingDto
            {
                CompanyName = setting.CompanyName,

                Logo = setting.Logo,

                Phone1 = setting.Phone1,
                Phone2 = setting.Phone2,
                Email = setting.Email,

                AddressAz = setting.AddressAz,
                AddressEn = setting.AddressEn,
                AddressRu = setting.AddressRu,

                WorkingHoursAz = setting.WorkingHoursAz,
                WorkingHoursEn = setting.WorkingHoursEn,
                WorkingHoursRu = setting.WorkingHoursRu,

                Facebook = setting.Facebook,
                Instagram = setting.Instagram,
                Youtube = setting.Youtube,
                YoutubeVideo1 = setting.YoutubeVideo1,
                YoutubeVideo2 = setting.YoutubeVideo2,
                TikTok = setting.TikTok,
                Telegram = setting.Telegram,
                WhatsApp = setting.WhatsApp,

                GoogleMap = setting.GoogleMap,

                FooterTextAz = setting.FooterTextAz,
                FooterTextEn = setting.FooterTextEn,
                FooterTextRu = setting.FooterTextRu,

                DefaultSeoTitleAz = setting.DefaultSeoTitleAz,
                DefaultSeoTitleEn = setting.DefaultSeoTitleEn,
                DefaultSeoTitleRu = setting.DefaultSeoTitleRu,

                DefaultSeoDescriptionAz = setting.DefaultSeoDescriptionAz,
                DefaultSeoDescriptionEn = setting.DefaultSeoDescriptionEn,
                DefaultSeoDescriptionRu = setting.DefaultSeoDescriptionRu,

                DefaultSeoKeywordsAz = setting.DefaultSeoKeywordsAz,
                DefaultSeoKeywordsEn = setting.DefaultSeoKeywordsEn,
                DefaultSeoKeywordsRu = setting.DefaultSeoKeywordsRu
            }
        };

        ViewData["Title"] = "Settings";

        return View(model);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(UpdateSettingViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? oldLogo = model.Setting.Logo;
        bool uploadedNewLogo = false;

        if (model.LogoFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.LogoFile,
                "settings");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.LogoFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Setting.Logo = uploadResult.FilePath;
            uploadedNewLogo = true;
        }

        try
        {
            await _settingService.UpdateAsync(model.Setting);

            if (uploadedNewLogo &&
                !string.IsNullOrWhiteSpace(oldLogo))
            {
                await _fileService.DeleteAsync(oldLogo);
            }

            TempData["Success"] = "Settings updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (uploadedNewLogo &&
                !string.IsNullOrWhiteSpace(model.Setting.Logo))
            {
                await _fileService.DeleteAsync(model.Setting.Logo);

                model.Setting.Logo = oldLogo;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }
}
