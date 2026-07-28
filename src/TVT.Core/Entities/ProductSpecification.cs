namespace TVT.Core.Entities;

public class ProductSpecification : BaseEntity
{
    public int ProductId { get; set; }

    public int SpecificationId { get; set; }

    public string ValueAz { get; set; } = null!;

    public string ValueEn { get; set; } = null!;

    public string ValueRu { get; set; } = null!;


    // Navigation Properties
    public Product Product { get; set; } = null!;

    public Specification Specification { get; set; } = null!;
}
