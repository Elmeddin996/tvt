using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.DTOs.Products;

namespace TVT.Web.Areas.Admin.ViewModels.Products;

public class UpdateProductViewModel
{
    public UpdateProductDto Product { get; set; } = new();

    public IFormFile? MainImage { get; set; }

    public List<SelectListItem> Categories { get; set; } = [];

    public List<SelectListItem> Brands { get; set; } = [];
}
