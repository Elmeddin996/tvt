namespace TVT.Web.Areas.Admin.ViewModels.Products;

public class UpdateProductSpecificationViewModel
{
    public int SpecificationId { get; set; }

    public string GroupName { get; set; } = null!;

    public string SpecificationName { get; set; } = null!;

    public string? ValueAz { get; set; }

    public string? ValueEn { get; set; }

    public string? ValueRu { get; set; }
}
