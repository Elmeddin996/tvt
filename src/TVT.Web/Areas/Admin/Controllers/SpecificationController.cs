using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Specifications;
using TVT.Web.Areas.Admin.ViewModels.Specifications;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SpecificationController : BaseAdminController
{
    private readonly ISpecificationService _specificationService;
    private readonly ISpecificationGroupService _specificationGroupService;

    public SpecificationController(
        ISpecificationService specificationService,
        ISpecificationGroupService specificationGroupService)
    {
        _specificationService = specificationService;
        _specificationGroupService = specificationGroupService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Specifications";

        var specifications = await _specificationService.GetAllAsync();

        return View(specifications);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create Specification";

        var model = new CreateSpecificationViewModel();

        await LoadDropdowns(model);

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSpecificationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            await _specificationService.CreateAsync(model.Specification);

            TempData["Success"] = "Specification created successfully.";

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
        var specification = await _specificationService.GetByIdAsync(id);

        if (specification is null)
            return NotFound();

        var model = new UpdateSpecificationViewModel
        {
            Specification = new UpdateSpecificationDto
            {
                Id = specification.Id,
                SpecificationGroupId = specification.SpecificationGroupId,
                NameAz = specification.NameAz,
                NameEn = specification.NameEn,
                NameRu = specification.NameRu,
                DisplayOrder = specification.DisplayOrder
            }
        };

        await LoadDropdowns(model);

        ViewData["Title"] = "Edit Specification";

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateSpecificationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            await _specificationService.UpdateAsync(model.Specification);

            TempData["Success"] = "Specification updated successfully.";

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
        var specification = await _specificationService.GetByIdAsync(id);

        if (specification is null)
            return NotFound();

        try
        {
            await _specificationService.DeleteAsync(id);

            TempData["Success"] = "Specification deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns(CreateSpecificationViewModel model)
    {
        var groups = await _specificationGroupService.GetAllAsync();

        model.SpecificationGroups = groups
            .Where(x => x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();

        model.SpecificationGroups.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "-- Select Specification Group --"
        });
    }

    private async Task LoadDropdowns(UpdateSpecificationViewModel model)
    {
        var groups = await _specificationGroupService.GetAllAsync();

        model.SpecificationGroups = groups
            .Where(x => x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();

        model.SpecificationGroups.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "-- Select Specification Group --"
        });
    }
}
