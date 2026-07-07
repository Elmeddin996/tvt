namespace TVT.Core.Entities;

public class ProductSpecification : BaseEntity
{
    public int ProductId { get; set; }
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;
    public string ValueAz { get; set; } = null!;
    public string ValueEn { get; set; } = null!;
    public string ValueRu { get; set; } = null!;
    public int DisplayOrder { get; set; }

    public Product Product { get; set; } = null!;
}
