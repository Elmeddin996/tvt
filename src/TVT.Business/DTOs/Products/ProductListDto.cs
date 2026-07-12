namespace TVT.Business.DTOs.Products;

public class ProductListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
}
