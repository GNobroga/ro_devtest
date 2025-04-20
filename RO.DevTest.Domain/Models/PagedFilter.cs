namespace RO.DevTest.Domain.Models;

public enum SortOrder {
    Ascending,
    Descending,
}

public class PagedFilter {
    private const string DefaultSortBy = "Id";
    private const int DefaultPageSize = 100;

    private int _page = 1;
    private int _pageSize = DefaultPageSize;
    private SortOrder _sortOrder = SortOrder.Ascending;

    public string Keyword { get; set; } = string.Empty;

    public int Page {
        get => _page;
        set => _page = Math.Max(1, value);
    }

    public int PageSize {
        get => _pageSize;
        set => _pageSize = value < 1 ? DefaultPageSize : value;
    }

    public string SortBy { get; set; } = DefaultSortBy;

    public string Sort {
        get => _sortOrder == SortOrder.Ascending ? "asc" : "desc";
        set => _sortOrder = ParseSortOrder(value);
    }

    public SortOrder SortOrder => _sortOrder;

    public bool IsSortOrderAscending => _sortOrder == SortOrder.Ascending;

    private static SortOrder ParseSortOrder(string? value) {
        return value?.ToLowerInvariant() switch
        {
            "desc" => SortOrder.Descending,
            _ => SortOrder.Ascending
        };
    }
}
