namespace TVT.Business.DTOs.Orders;

public class CreateOrderDto
{
    public string CustomerName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public List<CreateOrderItemDto> Items { get; set; } = new();
}
