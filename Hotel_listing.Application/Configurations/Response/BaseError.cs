namespace Hotel_listing.Application.Configurations.Response;

public class BaseError
{
    public Guid ErrorId { get; set; } = Guid.NewGuid();
    public string ErrorMessage { get; set; }
}