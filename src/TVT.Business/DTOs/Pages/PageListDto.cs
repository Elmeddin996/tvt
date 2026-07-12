namespace TVT.Business.DTOs.Pages;

public class PageListDto
{
    public int Id { get; set; }
    public string TitleAz { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string TitleRu { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string SlugRu { get; set; } = null!;
}