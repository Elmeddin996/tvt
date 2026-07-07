namespace TVT.Core.Entities;

public class Brand : BaseEntity
{
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionRu { get; set; }
    public string? Logo { get; set; }
    public string? Website { get; set; }
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
