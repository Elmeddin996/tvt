namespace TVT.Business.DTOs.Orders;

public class OrderDetailDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public List<OrderItemDto> Items { get; set; } = new();
}
