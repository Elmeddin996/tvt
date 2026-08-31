namespace TVT.Business.DTOs.MobileSliders;

public class CreateMobileSliderDto
{
    public string? Image { get; set; }

    public string? Link { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;
}
