using Hotel_listing.Application.Common.RepositoryOptions;

namespace Hotel_listing.Application.Common.Features;

public class DataAccessorOptions<TParams>
{
    private string _connectionId { get; set; }
    public Sqltype SqlType { get; set; }
    public string Sql { get; set; }
    public TParams Prams { get; set; }

    public string ConnectionId
    {
        get => _connectionId;
        set => _connectionId = value == null ? "Default" : value;
    }
}