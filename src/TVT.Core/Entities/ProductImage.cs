namespace TVT.Core.Entities;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }
    public string Image { get; set; } = null!;
    public bool IsMain { get; set; }
    public int DisplayOrder { get; set; }

    public Product Product { get; set; } = null!;
}
