namespace TVT.Business.DTOs.Subscribers;

public class UpdateSubscriberDto
{
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }
}
