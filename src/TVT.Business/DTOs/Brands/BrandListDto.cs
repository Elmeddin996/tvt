namespace TVT.Business.DTOs.Brands;

public class BrandListDto
{
    public int Id { get; set; }
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string NameRu { get; set; } = null!;
    public string? Logo { get; set; }
}