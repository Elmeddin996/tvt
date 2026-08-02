using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.DTOs.Specifications;

namespace TVT.Web.Areas.Admin.ViewModels.Specifications;

public class UpdateSpecificationViewModel
{
    public UpdateSpecificationDto Specification { get; set; } = new();

    public List<SelectListItem> SpecificationGroups { get; set; } = new();
}
