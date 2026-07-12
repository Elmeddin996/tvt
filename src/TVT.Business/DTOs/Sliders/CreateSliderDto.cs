namespace TVT.Business.DTOs.Sliders;

public class CreateSliderDto
{
    public string? TitleAz { get; set; }
    public string? TitleEn { get; set; }
    public string? TitleRu { get; set; }
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionRu { get; set; }
    public string Image { get; set; } = null!;
    public string? ButtonTextAz { get; set; }
    public string? ButtonTextEn { get; set; }
    public string? ButtonTextRu { get; set; }
    public string? ButtonLink { get; set; }
    public int DisplayOrder { get; set; }
}
