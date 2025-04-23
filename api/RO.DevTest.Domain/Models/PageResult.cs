namespace RO.DevTest.Domain.Models;

public class PageResult<T> where T: class {

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int) Math.Ceiling((double)TotalItems / PageSize);
    public List<T> Items { get; set; } = [];

    public PageResult() {}

    public PageResult(int page, int size, int total, List<T> items) {
        Page = page;
        PageSize = size;
        TotalItems = total;
        Items = items;
    }

    public PageResult<U> Map<U>(Func<T, U> mapper) where U : class {
        var mappedItems = Items.Select(mapper).ToList();
        return new PageResult<U>(Page, PageSize, TotalItems, mappedItems);
    }


}