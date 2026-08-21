using Microsoft.AspNetCore.Mvc;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Web.Areas.Admin.ViewModels.Categories;
using TVT.Web.Services;
using TVT.Core.Common.Pagination;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IFileService _fileService;

    public CategoryController(
     ICategoryService categoryService,
     IFileService fileService)
    {
        _categoryService = categoryService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        ViewData["Title"] = "Categories";

        var request = new PagedRequest
        {
            Page = page,
            Search = search
        };

        var categories = await _categoryService.GetPagedAsync(request);

        ViewBag.Search = search;

        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create Category";

        var model = new CreateCategoryViewModel
        {
            ParentCategories = await GetParentCategorySelectListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.ParentCategories =
                await GetParentCategorySelectListAsync(model.Category.ParentId);

            return View(model);
        }

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "categories");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                model.ParentCategories =
                    await GetParentCategorySelectListAsync(model.Category.ParentId);

                return View(model);
            }

            model.Category.Image = uploadResult.FilePath;
        }

        if (model.IconFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.IconFile,
                "categories/icons");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.IconFile),
                    uploadResult.ErrorMessage!);

                model.ParentCategories =
                    await GetParentCategorySelectListAsync(model.Category.ParentId);

                return View(model);
            }

            model.Category.Icon = uploadResult.FilePath;
        }

        await _categoryService.CreateAsync(model.Category);

        TempData["Success"] = "Category created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        var model = new EditCategoryViewModel
        {
            Category = new UpdateCategoryDto
            {
                Id = category.Id,
                ParentId = category.ParentId,

                NameAz = category.NameAz,
                NameEn = category.NameEn,
                NameRu = category.NameRu,

                SlugAz = category.SlugAz,
                SlugEn = category.SlugEn,
                SlugRu = category.SlugRu,

                DescriptionAz = category.DescriptionAz,
                DescriptionEn = category.DescriptionEn,
                DescriptionRu = category.DescriptionRu,

                Image = category.Image,
                Icon = category.Icon,

                IsActive = category.IsActive
            },

            ParentCategories = await GetParentCategorySelectListAsync(
    selectedValue: category.ParentId,
    excludeCategoryId: category.Id)
        };

        ViewData["Title"] = "Edit Category";

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.ParentCategories =
                await GetParentCategorySelectListAsync(
                    selectedValue: model.Category.ParentId,
                    excludeCategoryId: model.Category.Id);

            return View(model);
        }

        string? oldImage = model.Category.Image;
        string? oldIcon = model.Category.Icon;

        bool uploadedNewImage = false;
        bool uploadedNewIcon = false;

        if (model.ImageFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.ImageFile,
                "categories");

            if (!uploadResult.Success)
            {
                ModelState.AddModelError(
                    nameof(model.ImageFile),
                    uploadResult.ErrorMessage!);

                model.ParentCategories =
                    await GetParentCategorySelectListAsync(
                        selectedValue: model.Category.ParentId,
                        excludeCategoryId: model.Category.Id);

                return View(model);
            }

            model.Category.Image = uploadResult.FilePath;
            uploadedNewImage = true;
        }

        if (model.IconFile != null)
        {
            var uploadResult = await _fileService.UploadAsync(
                model.IconFile,
                "categories/icons");

            if (!uploadResult.Success)
            {
                if (uploadedNewImage)
                {
                    await _fileService.DeleteAsync(model.Category.Image);
                    model.Category.Image = oldImage;
                }

                ModelState.AddModelError(
                    nameof(model.IconFile),
                    uploadResult.ErrorMessage!);

                model.ParentCategories =
                    await GetParentCategorySelectListAsync(
                        selectedValue: model.Category.ParentId,
                        excludeCategoryId: model.Category.Id);

                return View(model);
            }

            model.Category.Icon = uploadResult.FilePath;
            uploadedNewIcon = true;
        }

        try
        {
            await _categoryService.UpdateAsync(model.Category);

            if (uploadedNewImage &&
                !string.IsNullOrWhiteSpace(oldImage))
            {
                await _fileService.DeleteAsync(oldImage);
            }

            if (uploadedNewIcon &&
                !string.IsNullOrWhiteSpace(oldIcon))
            {
                await _fileService.DeleteAsync(oldIcon);
            }

            TempData["Success"] = "Category updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            if (uploadedNewImage)
            {
                await _fileService.DeleteAsync(model.Category.Image);
                model.Category.Image = oldImage;
            }

            if (uploadedNewIcon)
            {
                await _fileService.DeleteAsync(model.Category.Icon);
                model.Category.Icon = oldIcon;
            }

            ModelState.AddModelError(string.Empty, ex.Message);

            model.ParentCategories =
                await GetParentCategorySelectListAsync(
                    selectedValue: model.Category.ParentId,
                    excludeCategoryId: model.Category.Id);

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        try
        {
            await _categoryService.DeleteAsync(id);

            if (!string.IsNullOrWhiteSpace(category.Image))
            {
                await _fileService.DeleteAsync(category.Image);
            }

            if (!string.IsNullOrWhiteSpace(category.Icon))
            {
                await _fileService.DeleteAsync(category.Icon);
            }

            TempData["Success"] = "Category deleted successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<List<SelectListItem>> GetParentCategorySelectListAsync(
     int? selectedValue = null,
     int? excludeCategoryId = null)
    {
        var categories = await _categoryService.GetParentCategoriesAsync(excludeCategoryId);

        return categories
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz,
                Selected = x.Id == selectedValue
            })
            .ToList();
    }
}
