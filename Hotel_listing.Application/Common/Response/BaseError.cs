namespace Hotel_listing.Application.Common.Response;

public class BaseError
{
    public Guid ErrorId { get; set; } = Guid.NewGuid();
    public string ErrorMessage { get; set; }
}