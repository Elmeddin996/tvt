using TVT.Business.DTOs.ProductImages;
using TVT.Business.DTOs.ProductSpecifications;

namespace TVT.Business.DTOs.Products;

public class ProductDetailDto
{
    public int Id { get; set; }

    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; } = null!;

    public string Code { get; set; } = null!;
    public string? Model { get; set; }

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

    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }

    public string? SeoTitleAz { get; set; }
    public string? SeoTitleEn { get; set; }
    public string? SeoTitleRu { get; set; }

    public string? SeoDescriptionAz { get; set; }
    public string? SeoDescriptionEn { get; set; }
    public string? SeoDescriptionRu { get; set; }

    public string? SeoKeywordsAz { get; set; }
    public string? SeoKeywordsEn { get; set; }
    public string? SeoKeywordsRu { get; set; }

    public List<ProductImageDto> Images { get; set; } = new();

    public List<ProductSpecificationDto> Specifications { get; set; } = new();
}
