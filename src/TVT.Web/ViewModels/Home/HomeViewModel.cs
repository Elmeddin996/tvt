using TVT.Business.DTOs.Sliders;

namespace TVT.Web.ViewModels.Home;

public class HomeViewModel
{
    public IReadOnlyList<SliderDetailDto> Sliders { get; set; } = [];
}
