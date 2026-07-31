using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.SpecificationGroups;
using TVT.Web.Areas.Admin.ViewModels.SpecificationGroups;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SpecificationGroupController : Controller
{
    private readonly ISpecificationGroupService _specificationGroupService;

    public SpecificationGroupController(
        ISpecificationGroupService specificationGroupService)
    {
        _specificationGroupService = specificationGroupService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Specification Groups";

        var specificationGroups = await _specificationGroupService.GetAllAsync();

        return View(specificationGroups);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create Specification Group";

        var model = new CreateSpecificationGroupViewModel();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSpecificationGroupViewModel model)
    {
        if (!ModelState.IsValid)
        {
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
                DisplayOrder = specificationGroup.DisplayOrder
            }
        };

        ViewData["Title"] = "Edit Specification Group";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateSpecificationGroupViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _specificationGroupService.UpdateAsync(model.SpecificationGroup);

            TempData["Success"] = "Specification group updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

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
}
