namespace Hotel_listing.Application.Contracts.RepositoryManager.Options;

public class PaginationParams
{
    private int _pageSize;
    private int _pageNumber;
    private int _maxPageSize;

    public int MaxPageSize
    {
        get => _maxPageSize == 0 ? 50 : _maxPageSize;
        set
        {
            if (value == 0)
            {
                _maxPageSize = 50;
                if (_maxPageSize < _pageSize)
                {
                    _pageSize = _maxPageSize;
                }
            }
            else
            {
                _maxPageSize = value;
                if (_maxPageSize < _pageSize)
                {
                    _pageSize = _maxPageSize;
                }
            }
        }
    }

    public int PageSize
    {
        get => _pageSize == 0 ? MaxPageSize : _pageSize > _maxPageSize ? MaxPageSize : _pageSize;
        
        set
        {
            if (value > MaxPageSize)
            {
                _pageSize = MaxPageSize;
            }
            else if (value == 0)
            {
                _pageSize = MaxPageSize;
            }
            else
            {
                _pageSize = value;
            }
        }
    }

    public int PageNumber
    {
        get => _pageNumber == 0 ? 1 : _pageNumber;
        set => _pageNumber = value == 0 ? 1 : value;
    }
}