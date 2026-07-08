namespace TVT.Core.Entities;

public class Page : BaseEntity
{
    public string TitleAz { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string TitleRu { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;
    public string ContentAz { get; set; } = null!;
    public string ContentEn { get; set; } = null!;
    public string ContentRu { get; set; } = null!;
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
