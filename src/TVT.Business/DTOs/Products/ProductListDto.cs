namespace TVT.Business.DTOs.Products;

public class ProductListDto
{
    public int Id { get; set; }

    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;

    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Model { get; set; }

    public string CategoryName { get; set; } = null!;

    public string BrandName { get; set; } = null!;

    public string? MainImage { get; set; }

    public decimal Price { get; set; }

    public decimal? OldPrice { get; set; }

    public int StockQuantity { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsNew { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }
}
