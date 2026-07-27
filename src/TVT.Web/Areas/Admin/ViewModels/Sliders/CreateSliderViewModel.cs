using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.Sliders;

namespace TVT.Web.Areas.Admin.ViewModels.Sliders;

public class CreateSliderViewModel
{
    public CreateSliderDto Slider { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
