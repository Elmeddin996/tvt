using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.SpecificationGroups;
using TVT.Web.Areas.Admin.ViewModels.SpecificationGroups;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SpecificationGroupController : BaseAdminController
{
    private readonly ISpecificationGroupService _specificationGroupService;
    private readonly ICategoryService _categoryService;

    public SpecificationGroupController(
        ISpecificationGroupService specificationGroupService,
        ICategoryService categoryService)
    {
        _specificationGroupService = specificationGroupService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Specification Groups";

        var specificationGroups = await _specificationGroupService.GetAllAsync();

        return View(specificationGroups);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create Specification Group";

        var model = new CreateSpecificationGroupViewModel();

        await LoadDropdowns(model);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSpecificationGroupViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            await _specificationGroupService.CreateAsync(model.SpecificationGroup);

            TempData["Success"] = "Specification group created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadDropdowns(model);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var specificationGroup = await _specificationGroupService.GetByIdAsync(id);

        if (specificationGroup is null)
            return NotFound();

        var model = new UpdateSpecificationGroupViewModel
        {
            SpecificationGroup = new UpdateSpecificationGroupDto
            {
                Id = specificationGroup.Id,
                NameAz = specificationGroup.NameAz,
                NameEn = specificationGroup.NameEn,
                NameRu = specificationGroup.NameRu,
                CategoryIds = specificationGroup.CategoryIds,
                DisplayOrder = specificationGroup.DisplayOrder
            }
        };

        await LoadDropdowns(model);

        ViewData["Title"] = "Edit Specification Group";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateSpecificationGroupViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            await _specificationGroupService.UpdateAsync(model.SpecificationGroup);

            TempData["Success"] = "Specification group updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadDropdowns(model);

            return View(model);
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var specificationGroup = await _specificationGroupService.GetByIdAsync(id);

        if (specificationGroup is null)
            return NotFound();

        try
        {
            await _specificationGroupService.DeleteAsync(id);

            TempData["Success"] = "Specification group deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns(CreateSpecificationGroupViewModel model)
    {
        var categories = await _categoryService.GetAllAsync();

        model.Categories = categories
            .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();
    }

    private async Task LoadDropdowns(UpdateSpecificationGroupViewModel model)
    {
        var categories = await _categoryService.GetAllAsync();

        model.Categories = categories
            .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();
    }
}
