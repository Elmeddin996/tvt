using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Brands;
using TVT.Core.Common.Pagination;
using TVT.Web.Areas.Admin.ViewModels.Brands;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    private readonly IFileService _fileService;

    public BrandController(
        IBrandService brandService,
        IFileService fileService)
    {
        _brandService = brandService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        ViewData["Title"] = "Brands";

        var request = new PagedRequest
        {
            Page = page,
            Search = search
        };

        var brands = await _brandService.GetPagedAsync(request);

        ViewBag.Search = search;

        return View(brands);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Create Brand";

        return View(new CreateBrandViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBrandViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.LogoFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.LogoFile,
                "brands");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.LogoFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Brand.Logo = uploadResult.FilePath;
        }

        try
        {
            await _brandService.CreateAsync(model.Brand);

            TempData["Success"] = "Brand created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            if (!string.IsNullOrWhiteSpace(model.Brand.Logo))
            {
                await _fileService.DeleteAsync(model.Brand.Logo);
                model.Brand.Logo = null;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var brand = await _brandService.GetByIdAsync(id);

        if (brand == null)
            return NotFound();

        var model = new EditBrandViewModel
        {
            Brand = new UpdateBrandDto
            {
                Id = brand.Id,

                NameAz = brand.NameAz,
                NameEn = brand.NameEn,
                NameRu = brand.NameRu,

                SlugAz = brand.SlugAz,
                SlugEn = brand.SlugEn,
                SlugRu = brand.SlugRu,

                DescriptionAz = brand.DescriptionAz,
                DescriptionEn = brand.DescriptionEn,
                DescriptionRu = brand.DescriptionRu,

                Logo = brand.Logo,

                Website = brand.Website,

                IsActive = brand.IsActive
            }
        };

        ViewData["Title"] = "Edit Brand";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditBrandViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? oldLogo = model.Brand.Logo;
        bool uploadedNewLogo = false;

        if (model.LogoFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.LogoFile,
                "brands");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.LogoFile),
                    uploadResult.ErrorMessage!);

                return View(model);
            }

            model.Brand.Logo = uploadResult.FilePath;
            uploadedNewLogo = true;
        }

        try
        {
            await _brandService.UpdateAsync(model.Brand);

            if (uploadedNewLogo)
            {
                await _fileService.DeleteAsync(oldLogo);
            }

            TempData["Success"] = "Brand updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            if (uploadedNewLogo)
            {
                await _fileService.DeleteAsync(model.Brand.Logo);
                model.Brand.Logo = oldLogo;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _brandService.GetByIdAsync(id);

        if (brand == null)
            return NotFound();

        try
        {
            await _brandService.DeleteAsync(id);

            if (!string.IsNullOrWhiteSpace(brand.Logo))
            {
                await _fileService.DeleteAsync(brand.Logo);
            }

            TempData["Success"] = "Brand deleted successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
