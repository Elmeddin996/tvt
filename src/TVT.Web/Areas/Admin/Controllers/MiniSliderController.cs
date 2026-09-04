using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.MiniSliders;
using TVT.Core.Common.Pagination;
using TVT.Web.Areas.Admin.ViewModels.MiniSliders;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MiniSliderController : BaseAdminController
{
    private readonly IMiniSliderService _miniSliderService;
    private readonly IFileService _fileService;

    public MiniSliderController(
        IMiniSliderService miniSliderService,
        IFileService fileService)
    {
        _miniSliderService = miniSliderService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        ViewData["Title"] = "Mini Sliders";

        var request = new PagedRequest
        {
            Page = page
        };

        var miniSliders = await _miniSliderService
            .GetPagedAsync(request);

        return View(miniSliders);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create Mini Slider";

        return View(new CreateMiniSliderViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMiniSliderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "mini-sliders");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.MiniSlider.Image = uploadResult.FilePath;
        }

        try
        {
            await _miniSliderService.CreateAsync(model.MiniSlider);

            TempData["Success"] = "Mini slider created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrWhiteSpace(model.MiniSlider.Image))
            {
                await _fileService.DeleteAsync(model.MiniSlider.Image);
                model.MiniSlider.Image = null!;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }



    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var miniSlider = await _miniSliderService.GetByIdAsync(id);

        if (miniSlider == null)
            return NotFound();

        var model = new EditMiniSliderViewModel
        {
            MiniSlider = new UpdateMiniSliderDto
            {
                Id = miniSlider.Id,
                Image = miniSlider.Image,
                Link = miniSlider.Link,
                SortOrder = miniSlider.SortOrder,
                IsActive = miniSlider.IsActive
            }
        };

        ViewData["Title"] = "Edit Mini Slider";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditMiniSliderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? oldImage = model.MiniSlider.Image;
        bool uploadedNewImage = false;

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "mini-sliders");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.MiniSlider.Image = uploadResult.FilePath;
            uploadedNewImage = true;
        }

        try
        {
            await _miniSliderService.UpdateAsync(model.MiniSlider);

            if (uploadedNewImage)
            {
                await _fileService.DeleteAsync(oldImage);
            }

            TempData["Success"] = "Mini slider updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            if (uploadedNewImage)
            {
                await _fileService.DeleteAsync(model.MiniSlider.Image);
                model.MiniSlider.Image = oldImage;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var miniSlider = await _miniSliderService.GetByIdAsync(id);

        if (miniSlider == null)
            return NotFound();

        try
        {
            await _miniSliderService.DeleteAsync(id);

            if (!string.IsNullOrWhiteSpace(miniSlider.Image))
            {
                await _fileService.DeleteAsync(miniSlider.Image);
            }

            TempData["Success"] = "Mini slider deleted successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
