namespace TVT.Business.DTOs.MiniSliders;

public class UpdateMiniSliderDto
{
    public int Id { get; set; }

    public string? Image { get; set; } = null!;

    public string? Link { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;
}
