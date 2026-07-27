using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.DTOs.Categories;

namespace TVT.Web.Areas.Admin.ViewModels.Categories;

public class EditCategoryViewModel
{
    public UpdateCategoryDto Category { get; set; } = new();

    public IFormFile? ImageFile { get; set; }

    public List<SelectListItem> ParentCategories { get; set; } = [];
}
