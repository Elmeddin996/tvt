namespace TVT.Business.DTOs.Subscribers;

public class SubscriberDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }
}
