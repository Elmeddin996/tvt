namespace TVT.Core.Entities;

public class Specification : BaseEntity
{
    public int SpecificationGroupId { get; set; }

    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public int DisplayOrder { get; set; }

    // Navigation Properties
    public SpecificationGroup SpecificationGroup { get; set; } = null!;

    public ICollection<ProductSpecification> ProductSpecifications { get; set; } = new List<ProductSpecification>();
}
