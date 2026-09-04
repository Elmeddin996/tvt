using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.Abstractions.Services;
using TVT.Business.DTOs.ProductImages;
using TVT.Business.DTOs.Products;
using TVT.Business.DTOs.ProductSpecifications;
using TVT.Business.Services;
using TVT.Web.Areas.Admin.ViewModels.Products;
using TVT.Web.Services;

namespace TVT.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : BaseAdminController
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly IProductImageService _productImageService;
    private readonly IFileService _fileService;
    private readonly IProductSpecificationService _productSpecificationService;

    public ProductController(
    IProductService productService,
    ICategoryService categoryService,
    IBrandService brandService,
     IFileService fileService,
     IProductSpecificationService productSpecificationService,
    IProductImageService productImageService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _productImageService = productImageService;
        _fileService = fileService;
        _productSpecificationService = productSpecificationService;
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
            var productId = await _productService.CreateAsync(model.Product);

            TempData["Success"] = "Product created successfully.";

            return RedirectToAction(nameof(Edit), new { id = productId });
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

        model.Images = await _productImageService.GetByProductIdAsync(id);

        model.Specifications = (await _productSpecificationService
            .GetByProductIdAsync(id))
            .Select(x => new UpdateProductSpecificationViewModel
            {
                SpecificationId = x.SpecificationId,
                GroupName = x.GroupName,
                SpecificationName = x.SpecificationName,
                ValueAz = x.ValueAz,
                ValueEn = x.ValueEn,
                ValueRu = x.ValueRu
            })
            .ToList();

        model.UploadImage.ProductId = id;
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

            throw new Exception(
    string.Join("\n",
        ModelState
            .Where(x => x.Value.Errors.Any())
            .SelectMany(x => x.Value.Errors.Select(e => $"{x.Key} => {e.ErrorMessage}"))));
            return View(model);
        }

        try
        {
            await _productService.UpdateAsync(model.Product);

            await _productSpecificationService.UpdateAsync(
                model.Product.Id,
                model.Specifications
                    .Select(x => new UpdateProductSpecificationDto
                    {
                        SpecificationId = x.SpecificationId,
                        ValueAz = x.ValueAz,
                        ValueEn = x.ValueEn,
                        ValueRu = x.ValueRu
                    })
                    .ToList());

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

        if (product is null)
            return NotFound();

        try
        {
            var images = await _productImageService.GetByProductIdAsync(id);

            foreach (var image in images)
            {
                await _fileService.DeleteAsync(image.Image);
            }

           
            await _productService.DeleteAsync(id);

            TempData["Success"] = "Product deleted successfully.";
        }
        catch (Exception ex)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadImage(UploadProductImageViewModel model)
    {

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please select an image.";

            return RedirectToAction(nameof(Edit), new { id = model.ProductId });
        }

        var uploadResult = await _fileService.UploadAsync(model.Image, "products");

        if (!uploadResult.Success)
        {
            TempData["Error"] = uploadResult.ErrorMessage;

            return RedirectToAction(nameof(Edit), new { id = model.ProductId });
        }

        await _productImageService.UploadAsync(new UploadProductImageDto
        {
            ProductId = model.ProductId,
            Image = uploadResult.FilePath!,
            IsMain = model.IsMain,
            DisplayOrder = model.DisplayOrder
        });

        TempData["Success"] = "Image uploaded successfully.";

        return RedirectToAction(nameof(Edit), new { id = model.ProductId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetMainImage(int imageId, int productId)
    {
        try
        {
            await _productImageService.SetMainImageAsync(imageId);

            TempData["Success"] = "Main image updated successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Edit), new { id = productId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteImage(int imageId, int productId)
    {
        try
        {
            var image = await _productImageService.GetByIdAsync(imageId);

            if (image is null)
                return NotFound();

            await _fileService.DeleteAsync(image.Image);

            await _productImageService.DeleteAsync(imageId);

            TempData["Success"] = "Image deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Edit), new { id = productId });
    }
}
