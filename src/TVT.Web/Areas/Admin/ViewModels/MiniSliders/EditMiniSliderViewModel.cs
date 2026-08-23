using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.MiniSliders;

namespace TVT.Web.Areas.Admin.ViewModels.MiniSliders;

public class EditMiniSliderViewModel
{
    public UpdateMiniSliderDto MiniSlider { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
