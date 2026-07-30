using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.DTOs.ProductImages;
using TVT.Business.DTOs.Products;

namespace TVT.Web.Areas.Admin.ViewModels.Products;

public class UpdateProductViewModel
{
    public UpdateProductDto Product { get; set; } = new();

    public List<ProductImageDto> Images { get; set; } = [];

    public UploadProductImageViewModel UploadImage { get; set; } = new();

    public List<SelectListItem> Categories { get; set; } = [];

    public List<SelectListItem> Brands { get; set; } = [];
}
