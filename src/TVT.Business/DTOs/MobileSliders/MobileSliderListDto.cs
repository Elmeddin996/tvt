namespace TVT.Business.DTOs.MobileSliders;

public class MobileSliderListDto
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;

    public string? Link { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }
}
