namespace TVT.Business.DTOs.Products;

public class ProductFilterOptionsDto
{
    public List<ProductFilterBrandDto> Brands { get; set; } = new();

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public int InStockCount { get; set; }

    public int OutOfStockCount { get; set; }
}

public class ProductFilterBrandDto
{
    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public int ProductCount { get; set; }
}
