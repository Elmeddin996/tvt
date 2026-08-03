namespace TVT.Core.Entities;

public class CategorySpecificationGroup
{
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public int SpecificationGroupId { get; set; }

    public SpecificationGroup SpecificationGroup { get; set; } = null!;
}
