namespace TVT.Business.DTOs.Categories;

public class UpdateCategoryDto
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

    public bool IsActive { get; set; } = true;

    public string? SeoTitleAz { get; set; }
    public string? SeoTitleEn { get; set; }
    public string? SeoTitleRu { get; set; }

    public string? SeoDescriptionAz { get; set; }
    public string? SeoDescriptionEn { get; set; }
    public string? SeoDescriptionRu { get; set; }

    public string? SeoKeywordsAz { get; set; }
    public string? SeoKeywordsEn { get; set; }
    public string? SeoKeywordsRu { get; set; }
}
