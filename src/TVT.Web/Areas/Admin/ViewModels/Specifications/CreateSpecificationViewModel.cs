using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.DTOs.Specifications;

namespace TVT.Web.Areas.Admin.ViewModels.Specifications;

public class CreateSpecificationViewModel
{
    public CreateSpecificationDto Specification { get; set; } = new();

    public List<SelectListItem> SpecificationGroups { get; set; } = new();
}
