namespace TVT.Business.DTOs.Orders;

public class OrderListDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }
    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
