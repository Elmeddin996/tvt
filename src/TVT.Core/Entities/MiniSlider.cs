namespace TVT.Core.Entities;

public class MiniSlider : BaseEntity
{
    public string Image { get; set; } = null!;

    public string? Link { get; set; }

    public int SortOrder { get; set; }

}
