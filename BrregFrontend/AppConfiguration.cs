using System.Net.Http.Json;

namespace BrregFrontend
{
    public class AppConfiguration
    {
        public string ApiUrl { get; set; } = "https://brreg.sindrema.com/api";
    }

    public class ConfigurationService
    {
        private AppConfiguration? _configuration;
        private readonly HttpClient _httpClient;

        public ConfigurationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppConfiguration> GetConfigurationAsync()
        {
            if (_configuration == null)
            {
                try
                {
                    _configuration = await _httpClient.GetFromJsonAsync<AppConfiguration>("config.json");
                }
                catch
                {
                    // Fallback to default configuration
                    _configuration = new AppConfiguration();
                }
            }
            return _configuration;
        }

        public string GetApiUrl()
        {
            return _configuration?.ApiUrl ?? "https://brreg.sindrema.com/api";
        }
    }
}
