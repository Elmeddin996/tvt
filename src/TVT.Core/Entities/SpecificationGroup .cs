namespace TVT.Core.Entities;

public class SpecificationGroup : BaseEntity
{
    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public ICollection<Specification> Specifications { get; set; } = new List<Specification>();
}
