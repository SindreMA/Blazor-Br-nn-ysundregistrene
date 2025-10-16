namespace BrregFrontend
{
    public class Statics
    {
        // Read API URL from environment variable, fallback to production URL
        public static string apiUrl = Environment.GetEnvironmentVariable("API_URL")
            ?? "https://brreg.sindrema.com/api";
    }
}
