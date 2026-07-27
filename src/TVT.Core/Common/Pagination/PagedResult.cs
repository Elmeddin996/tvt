namespace TVT.Core.Common.Pagination;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = new List<T>();

    public int CurrentPage { get; set; }

    public int PageSize { get; set; } = 20;

    public int TotalCount { get; set; }

    public int TotalPages =>
        (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasPreviousPage =>
        CurrentPage > 1;

    public bool HasNextPage =>
        CurrentPage < TotalPages;
}
