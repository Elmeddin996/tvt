namespace TVT.Core.Entities;

public class Product : BaseEntity
{
    public int BrandId { get; set; }
    public string Code { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionRu { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int StockQuantity { get; set; }
    public bool IsFeatured { get; set; } = false;
    public bool IsNew { get; set; } = false;
    public string? SeoTitleAz { get; set; }
    public string? SeoTitleEn { get; set; }
    public string? SeoTitleRu { get; set; }
    public string? SeoDescriptionAz { get; set; }
    public string? SeoDescriptionEn { get; set; }
    public string? SeoDescriptionRu { get; set; }
    public string? SeoKeywordsAz { get; set; }
    public string? SeoKeywordsEn { get; set; }
    public string? SeoKeywordsRu { get; set; }

    public Brand Brand { get; set; } = null!;
}
