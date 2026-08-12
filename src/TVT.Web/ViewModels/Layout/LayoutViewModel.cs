using TVT.Business.DTOs.Categories;
using TVT.Business.DTOs.Settings;

namespace TVT.Web.ViewModels.Layout;

public class LayoutViewModel
{
    public SettingDetailDto? Setting { get; set; }

    public List<CategoryListDto> Categories { get; set; } = [];
}
