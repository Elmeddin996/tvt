namespace TVT.Business.DTOs.Categories;

public class CategoryListDto
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string? ParentName { get; set; }

    public int DisplayOrder { get; set; }

    public string NameAz { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameRu { get; set; } = null!;

    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;

    public string? Image { get; set; }

    public string? Icon { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }
}
