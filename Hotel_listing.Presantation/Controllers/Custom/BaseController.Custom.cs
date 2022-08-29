namespace Hotel_listing.Presantation.Controllers;

public partial class BaseController<T> where T:class
{
    internal string TestMethode(string test)
    {
        return test;
    }
}