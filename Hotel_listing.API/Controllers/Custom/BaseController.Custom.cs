namespace Hotel_listing.API.Controllers;

public partial class BaseController<T> where T:class
{
    internal string TestMethode(string test)
    {
        return test;
    }
}