namespace TVT.Core.Common.Filters;

public class ProductFilterRequest
{
    public IReadOnlyCollection<int> BrandIds { get; set; }
        = Array.Empty<int>();

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public bool? InStock { get; set; }
}
