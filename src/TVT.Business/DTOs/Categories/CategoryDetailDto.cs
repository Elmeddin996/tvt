namespace TVT.Business.DTOs.Categories;

public class CategoryDetailDto
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int DisplayOrder { get; set; }

    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;

    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;

    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionRu { get; set; }

    public string? Image { get; set; }

    public string? Icon { get; set; }
    public bool IsActive { get; set; }
}
