using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.Brands;

namespace TVT.Web.Areas.Admin.ViewModels.Brands;

public class EditBrandViewModel
{
    public UpdateBrandDto Brand { get; set; } = new();

    public IFormFile? LogoFile { get; set; }
}
