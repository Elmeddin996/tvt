namespace TVT.Business.DTOs.Sliders;

public class SliderListDto
{
    public int Id { get; set; }
    public string? TitleAz { get; set; }
    public string? TitleEn { get; set; }
    public string? TitleRu { get; set; }
    public string Image { get; set; } = null!;
    public int DisplayOrder { get; set; }
}