using Microsoft.AspNetCore.Http;

namespace TVT.Web.Areas.Admin.ViewModels.Products;

public class UploadProductImageViewModel
{
    public int ProductId { get; set; }

    public IFormFile Image { get; set; } = null!;

    public bool IsMain { get; set; }

    public int DisplayOrder { get; set; }
}
