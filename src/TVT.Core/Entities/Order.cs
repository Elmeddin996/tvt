namespace TVT.Core.Entities;

public class Order : BaseEntity
{
    public string CustomerName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = "Pending";

    // Navigation Properties
    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}
