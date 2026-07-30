namespace TVT.Business.DTOs.ProductImages;

public class ProductImageDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Image { get; set; } = string.Empty;

    public bool IsMain { get; set; }

    public int DisplayOrder { get; set; }
}
