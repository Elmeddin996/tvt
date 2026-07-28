namespace TVT.Core.Entities;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    public string Image { get; set; } = null!;

    public bool IsMain { get; set; } = false;

    public int DisplayOrder { get; set; }

    // Navigation Property
    public Product Product { get; set; } = null!;
}
