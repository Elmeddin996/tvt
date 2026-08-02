namespace TVT.Business.DTOs.Specifications;

public class SpecificationListDto
{
    public int Id { get; set; }

    public int SpecificationGroupId { get; set; }

    public string SpecificationGroupName { get; set; } = null!;

    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }
}
