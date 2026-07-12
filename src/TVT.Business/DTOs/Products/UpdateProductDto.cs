namespace TVT.Business.DTOs.Products;

public class UpdateProductDto
{
    public int BrandId { get; set; }
    public string Code { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int StockQuantity { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
}
