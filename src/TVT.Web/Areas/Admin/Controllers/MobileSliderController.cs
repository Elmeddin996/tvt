using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.MobileSliders;
using TVT.Core.Common.Pagination;
using TVT.Web.Areas.Admin.ViewModels.MobileSliders;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MobileSliderController : Controller
{
    private readonly IMobileSliderService _mobileSliderService;
    private readonly IFileService _fileService;

    public MobileSliderController(
        IMobileSliderService mobileSliderService,
        IFileService fileService)
    {
        _mobileSliderService = mobileSliderService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        ViewData["Title"] = "Mobile Sliders";

        var request = new PagedRequest
        {
            Page = page
        };

        var mobileSliders = await _mobileSliderService
            .GetPagedAsync(request);

        return View(mobileSliders);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create Mobile Slider";

        return View("Create", new CreateMobileSliderViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateMobileSliderViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Create", model);

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "mobile-sliders");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View("Create", model);
            }

            model.MobileSlider.Image = uploadResult.FilePath;
        }

        if (string.IsNullOrWhiteSpace(model.MobileSlider.Image))
        {
            ModelState.AddModelError(
                nameof(model.ImageFile),
                "Mobile slider image is required.");

            return View("Create", model);
        }

        try
        {
            await _mobileSliderService.CreateAsync(
                model.MobileSlider);

            TempData["Success"] =
                "Mobile slider created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrWhiteSpace(model.MobileSlider.Image))
            {
                await _fileService.DeleteAsync(
                    model.MobileSlider.Image);

                model.MobileSlider.Image = null;
            }

            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View("Create", model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var mobileSlider =
            await _mobileSliderService.GetByIdAsync(id);

        if (mobileSlider == null)
            return NotFound();

        var model = new EditMobileSliderViewModel
        {
            MobileSlider = new UpdateMobileSliderDto
            {
                Id = mobileSlider.Id,
                Image = mobileSlider.Image,
                Link = mobileSlider.Link,
                SortOrder = mobileSlider.SortOrder,
                IsActive = mobileSlider.IsActive
            }
        };

        ViewData["Title"] = "Edit Mobile Slider";

        return View("Edit", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditMobileSliderViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Edit", model);

        string? oldImage = model.MobileSlider.Image;
        bool uploadedNewImage = false;

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "mobile-sliders");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View("Edit", model);
            }

            model.MobileSlider.Image =
                uploadResult.FilePath;

            uploadedNewImage = true;
        }

        try
        {
            await _mobileSliderService.UpdateAsync(
                model.MobileSlider);

            if (uploadedNewImage &&
                !string.IsNullOrWhiteSpace(oldImage))
            {
                await _fileService.DeleteAsync(oldImage);
            }

            TempData["Success"] =
                "Mobile slider updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (uploadedNewImage &&
                !string.IsNullOrWhiteSpace(model.MobileSlider.Image))
            {
                await _fileService.DeleteAsync(
                    model.MobileSlider.Image);
            }

            model.MobileSlider.Image = oldImage;

            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View("Edit", model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var mobileSlider =
            await _mobileSliderService.GetByIdAsync(id);

        if (mobileSlider == null)
            return NotFound();

        try
        {
            await _mobileSliderService.DeleteAsync(id);

            if (!string.IsNullOrWhiteSpace(
                mobileSlider.Image))
            {
                await _fileService.DeleteAsync(
                    mobileSlider.Image);
            }

            TempData["Success"] =
                "Mobile slider deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
