namespace TVT.Business.DTOs.ProductSpecifications;

public class ProductSpecificationDto
{
    public int SpecificationId { get; set; }

    public string GroupName { get; set; } = null!;

    public string SpecificationName { get; set; } = null!;

    public string? ValueAz { get; set; }

    public string? ValueEn { get; set; }

    public string? ValueRu { get; set; }

    public int DisplayOrder { get; set; }
    public int GroupDisplayOrder { get; set; }
}
