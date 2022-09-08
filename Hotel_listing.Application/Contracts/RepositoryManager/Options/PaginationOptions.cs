namespace Hotel_listing.Application.Contracts.RepositoryManager.Options;

public class PaginationOptions
{
    private int _pageSize;

    public int MaxPageSize { get; set; } = 50;
    public int PageNumber { get; set; }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > MaxPageSize)
            {
                _pageSize = MaxPageSize;
            }
            if (value == 0)
            {
                _pageSize = MaxPageSize;
            }
            _pageSize = value;
        }
    }
}