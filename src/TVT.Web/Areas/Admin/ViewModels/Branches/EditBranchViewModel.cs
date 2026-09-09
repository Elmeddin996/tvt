using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.Branches;

namespace TVT.Web.Areas.Admin.ViewModels.Branches;

public class EditBranchViewModel
{
    public UpdateBranchDto Branch { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
