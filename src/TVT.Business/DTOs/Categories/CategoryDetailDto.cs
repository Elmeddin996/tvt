namespace TVT.Business.DTOs.Categories;

public class CategoryDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
}
