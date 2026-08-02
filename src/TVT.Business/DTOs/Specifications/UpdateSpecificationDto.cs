namespace TVT.Business.DTOs.Specifications;

public class UpdateSpecificationDto
{
    public int Id { get; set; }

    public int SpecificationGroupId { get; set; }

    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public int DisplayOrder { get; set; }
}
