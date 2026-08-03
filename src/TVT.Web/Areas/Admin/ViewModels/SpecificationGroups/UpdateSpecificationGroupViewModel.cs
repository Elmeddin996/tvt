using Microsoft.AspNetCore.Mvc.Rendering;
using TVT.Business.DTOs.SpecificationGroups;

namespace TVT.Web.Areas.Admin.ViewModels.SpecificationGroups;

public class UpdateSpecificationGroupViewModel
{
    public UpdateSpecificationGroupDto SpecificationGroup { get; set; } = new();
    public List<SelectListItem> Categories { get; set; } = [];
}
