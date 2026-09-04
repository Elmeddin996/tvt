using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Sliders;
using TVT.Core.Common.Pagination;
using TVT.Web.Areas.Admin.ViewModels.Sliders;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : BaseAdminController
{
    private readonly ISliderService _sliderService;
    private readonly IFileService _fileService;

    public SliderController(
        ISliderService sliderService,
        IFileService fileService)
    {
        _sliderService = sliderService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        ViewData["Title"] = "Sliders";

        var request = new PagedRequest
        {
            Page = page,
            Search = search
        };

        var sliders = await _sliderService.GetPagedAsync(request);

        ViewBag.Search = search;

        return View(sliders);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create Slider";

        return View(new CreateSliderViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSliderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "sliders");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Slider.Image = uploadResult.FilePath;
        }

        try
        {
            await _sliderService.CreateAsync(model.Slider);

            TempData["Success"] = "Slider created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrWhiteSpace(model.Slider.Image))
            {
                await _fileService.DeleteAsync(model.Slider.Image);
                model.Slider.Image = null;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var slider = await _sliderService.GetByIdAsync(id);

        if (slider == null)
            return NotFound();

        var model = new EditSliderViewModel
        {
            Slider = new UpdateSliderDto
            {
                Id = slider.Id,

                TitleAz = slider.TitleAz,
                TitleEn = slider.TitleEn,
                TitleRu = slider.TitleRu,

                DescriptionAz = slider.DescriptionAz,
                DescriptionEn = slider.DescriptionEn,
                DescriptionRu = slider.DescriptionRu,

                Image = slider.Image,

                ButtonTextAz = slider.ButtonTextAz,
                ButtonTextEn = slider.ButtonTextEn,
                ButtonTextRu = slider.ButtonTextRu,

                ButtonLink = slider.ButtonLink,

                DisplayOrder = slider.DisplayOrder,

                IsActive = slider.IsActive
            }
        };

        ViewData["Title"] = "Edit Slider";

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditSliderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? oldImage = model.Slider.Image;
        bool uploadedNewImage = false;

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "sliders");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Slider.Image = uploadResult.FilePath;
            uploadedNewImage = true;
        }

        try
        {
            await _sliderService.UpdateAsync(model.Slider);

            if (uploadedNewImage &&
                !string.IsNullOrWhiteSpace(oldImage))
            {
                await _fileService.DeleteAsync(oldImage);
            }

            TempData["Success"] = "Slider updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            if (uploadedNewImage &&
                !string.IsNullOrWhiteSpace(model.Slider.Image))
            {
                await _fileService.DeleteAsync(model.Slider.Image);
                model.Slider.Image = oldImage;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _sliderService.GetByIdAsync(id);

        if (slider == null)
            return NotFound();

        try
        {
            await _sliderService.DeleteAsync(id);

            if (!string.IsNullOrWhiteSpace(slider.Image))
            {
                await _fileService.DeleteAsync(slider.Image);
            }

            TempData["Success"] = "Slider deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
