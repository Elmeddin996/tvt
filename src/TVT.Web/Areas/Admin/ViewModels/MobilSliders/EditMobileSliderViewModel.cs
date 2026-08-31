using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.MobileSliders;

namespace TVT.Web.Areas.Admin.ViewModels.MobileSliders;

public class EditMobileSliderViewModel
{
    public UpdateMobileSliderDto MobileSlider { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
