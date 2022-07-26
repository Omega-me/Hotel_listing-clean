namespace Hotel_listing.Application.Utilities
{
    public static class Utils
    {
        public static string GenerateBasePath(string directory)
        {
            var appPath = Environment.CurrentDirectory.Split("\\").SkipLast(1).ToArray();
            var path = String.Join("\\", appPath) + directory;
            return path;
        }
    }
}