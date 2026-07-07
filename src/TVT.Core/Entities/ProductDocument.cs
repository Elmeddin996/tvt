namespace TVT.Core.Entities;

public class ProductDocument : BaseEntity
{
    public int ProductId { get; set; }
    public string FileName { get; set; } = null!;
    public string File { get; set; } = null!;
    public int DisplayOrder { get; set; }

    public Product Product { get; set; } = null!;
}
