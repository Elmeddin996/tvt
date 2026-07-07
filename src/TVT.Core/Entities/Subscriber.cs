namespace TVT.Core.Entities;

public class Subscriber : BaseEntity
{
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; } = true;
}
