using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.MobileSliders;

namespace TVT.Web.Areas.Admin.ViewModels.MobileSliders;

public class CreateMobileSliderViewModel
{
    public CreateMobileSliderDto MobileSlider { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
