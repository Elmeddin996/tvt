namespace TVT.Business.DTOs.SpecificationGroups;

public class CreateSpecificationGroupDto
{
    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public int DisplayOrder { get; set; }
}
