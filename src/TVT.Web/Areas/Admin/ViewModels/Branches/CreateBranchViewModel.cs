using Microsoft.AspNetCore.Http;
using TVT.Business.DTOs.Branches;

namespace TVT.Web.Areas.Admin.ViewModels.Branches;

public class CreateBranchViewModel
{
    public CreateBranchDto Branch { get; set; } = new();

    public IFormFile? ImageFile { get; set; }
}
