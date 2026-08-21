namespace TVT.Core.Common.Filters;

public class ProductFilterOptions
{
    public List<ProductFilterBrand> Brands { get; set; } = new();

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public int InStockCount { get; set; }

    public int OutOfStockCount { get; set; }
}

public class ProductFilterBrand
{
    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public int ProductCount { get; set; }
}
