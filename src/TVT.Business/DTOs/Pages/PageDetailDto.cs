namespace TVT.Business.DTOs.Pages;

public class PageDetailDto
{
    public int Id { get; set; }
    public string TitleAz { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string TitleRu { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;
    public string ContentAz { get; set; } = null!;
    public string ContentEn { get; set; } = null!;
    public string ContentRu { get; set; } = null!;
}
