using TVT.Business.DTOs.Categories;
using TVT.Business.DTOs.MiniSliders;
using TVT.Business.DTOs.Products;
using TVT.Business.DTOs.Sliders;

namespace TVT.Web.ViewModels.Home;

public class HomeViewModel
{
    public IReadOnlyList<SliderDetailDto> Sliders { get; set; } = [];
    public IReadOnlyList<MiniSliderListDto> MiniSliders { get; set; } = [];
    public IReadOnlyList<CategoryListDto> Categories { get; set; } = [];
    public IReadOnlyList<ProductListDto> NewProducts { get; set; } = [];
}
