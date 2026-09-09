using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Branches;
using TVT.Core.Common.Pagination;
using TVT.Web.Areas.Admin.ViewModels.Branches;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class BranchController : BaseAdminController
{
    private readonly IBranchService _branchService;
    private readonly IFileService _fileService;

    public BranchController(
        IBranchService branchService,
        IFileService fileService)
    {
        _branchService = branchService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        ViewData["Title"] = "Branches";

        var request = new PagedRequest
        {
            Page = page,
            Search = search
        };

        var branches = await _branchService.GetPagedAsync(request);

        ViewBag.Search = search;

        return View(branches);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create Branch";

        return View(new CreateBranchViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateBranchViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "branches");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Branch.Image = uploadResult.FilePath;
        }

        await _branchService.CreateAsync(model.Branch);

        TempData["Success"] =
            "Branch created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var branch = await _branchService.GetByIdAsync(id);

        if (branch == null)
            return NotFound();

        var model = new EditBranchViewModel
        {
            Branch = new UpdateBranchDto
            {
                Id = branch.Id,

                DisplayOrder = branch.DisplayOrder,

                NameAz = branch.NameAz,
                NameEn = branch.NameEn,
                NameRu = branch.NameRu,

                AddressAz = branch.AddressAz,
                AddressEn = branch.AddressEn,
                AddressRu = branch.AddressRu,

                Phone = branch.Phone,
                Phone2 = branch.Phone2,

                WorkingHoursAz = branch.WorkingHoursAz,
                WorkingHoursEn = branch.WorkingHoursEn,
                WorkingHoursRu = branch.WorkingHoursRu,

                GoogleMapsUrl = branch.GoogleMapsUrl,

                Image = branch.Image,

                DescriptionAz = branch.DescriptionAz,
                DescriptionEn = branch.DescriptionEn,
                DescriptionRu = branch.DescriptionRu,

                IsActive = branch.IsActive
            }
        };

        ViewData["Title"] = "Edit Branch";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditBranchViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var oldImage = model.Branch.Image;
        var uploadedNewImage = false;

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "branches");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Branch.Image = uploadResult.FilePath;
            uploadedNewImage = true;
        }

        try
        {
            var updated =
                await _branchService.UpdateAsync(model.Branch);

            if (!updated)
            {
                if (uploadedNewImage)
                {
                    await _fileService.DeleteAsync(
                        model.Branch.Image);
                }

                return NotFound();
            }

            if (uploadedNewImage &&
                !string.IsNullOrWhiteSpace(oldImage))
            {
                await _fileService.DeleteAsync(oldImage);
            }

            TempData["Success"] =
                "Branch updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            if (uploadedNewImage)
            {
                await _fileService.DeleteAsync(
                    model.Branch.Image);

                model.Branch.Image = oldImage;
            }

            ModelState.AddModelError(
                string.Empty,
                "An error occurred while updating the branch.");

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var branch = await _branchService.GetByIdAsync(id);

        if (branch == null)
            return NotFound();

        await _branchService.DeleteAsync(id);

        if (!string.IsNullOrWhiteSpace(branch.Image))
        {
            await _fileService.DeleteAsync(branch.Image);
        }

        TempData["Success"] =
            "Branch deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}
