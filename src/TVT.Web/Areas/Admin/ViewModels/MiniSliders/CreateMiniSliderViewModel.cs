using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.MiniSliders;

namespace TVT.Web.Areas.Admin.ViewModels.MiniSliders;

public class CreateMiniSliderViewModel
{
    public CreateMiniSliderDto MiniSlider { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
