namespace TVT.Business.DTOs.Categories;

public class CategoryListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Image { get; set; }
}
