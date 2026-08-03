namespace TVT.Business.DTOs.SpecificationGroups;

public class UpdateSpecificationGroupDto
{
    public int Id { get; set; }

    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public ICollection<int> CategoryIds { get; set; } = new List<int>();

    public int DisplayOrder { get; set; }
}
