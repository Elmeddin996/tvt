using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.Products;
using TVT.Web.Areas.Admin.ViewModels.Products;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;

    public ProductController(
        IProductService productService,
        ICategoryService categoryService,
        IBrandService brandService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Products";

        var products = await _productService.GetAllAsync();

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create Product";

        var model = new CreateProductViewModel();

        await LoadDropdowns(model);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            await _productService.CreateAsync(model.Product);

            TempData["Success"] = "Product created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            await LoadDropdowns(model);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        var model = new UpdateProductViewModel
        {
            Product = new UpdateProductDto
            {
                Id = product.Id,

                CategoryId = product.CategoryId,
                BrandId = product.BrandId,

                Code = product.Code,
                Model = product.Model,

                NameAz = product.NameAz,
                NameEn = product.NameEn,
                NameRu = product.NameRu,

                DescriptionAz = product.DescriptionAz,
                DescriptionEn = product.DescriptionEn,
                DescriptionRu = product.DescriptionRu,

                Price = product.Price,
                OldPrice = product.OldPrice,
                StockQuantity = product.StockQuantity,

                IsFeatured = product.IsFeatured,
                IsNew = product.IsNew,

                SeoTitleAz = product.SeoTitleAz,
                SeoTitleEn = product.SeoTitleEn,
                SeoTitleRu = product.SeoTitleRu,

                SeoDescriptionAz = product.SeoDescriptionAz,
                SeoDescriptionEn = product.SeoDescriptionEn,
                SeoDescriptionRu = product.SeoDescriptionRu,

                SeoKeywordsAz = product.SeoKeywordsAz,
                SeoKeywordsEn = product.SeoKeywordsEn,
                SeoKeywordsRu = product.SeoKeywordsRu
            }
        };

        await LoadDropdowns(model);

        ViewData["Title"] = "Edit Product";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        try
        {
            await _productService.UpdateAsync(model.Product);

            TempData["Success"] = "Product updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
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
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        try
        {
            await _productService.DeleteAsync(id);

            TempData["Success"] = "Product deleted successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }


    private async Task LoadDropdowns(CreateProductViewModel model)
    {
        var categories = await _categoryService.GetAllAsync();
        var brands = await _brandService.GetAllAsync();

        model.Categories = categories
            .Where(x => x.IsActive)
            .OrderBy(x => x.NameAz)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();

        model.Categories.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "-- Select Category --"
        });

        model.Brands = brands
            .Where(x => x.IsActive)
            .OrderBy(x => x.NameAz)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();

        model.Brands.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "-- Select Brand --"
        });
    }

    private async Task LoadDropdowns(UpdateProductViewModel model)
    {
        var categories = await _categoryService.GetAllAsync();
        var brands = await _brandService.GetAllAsync();

        model.Categories = categories
            .Where(x => x.IsActive)
            .OrderBy(x => x.NameAz)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();

        model.Categories.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "-- Select Category --"
        });

        model.Brands = brands
            .Where(x => x.IsActive)
            .OrderBy(x => x.NameAz)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NameAz
            })
            .ToList();

        model.Brands.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "-- Select Brand --"
        });
    }

}
