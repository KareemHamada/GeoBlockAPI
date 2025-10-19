using GeoBlockAPI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace GeoBlockAPI.Services
{
    public class GeoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public GeoService(HttpClient httpClient, IOptions<GeoSettings> settings)
        {
            _httpClient = httpClient;
            _apiKey = settings.Value.ApiKey;
            _baseUrl = settings.Value.BaseUrl;
        }

        public async Task<IpInfo?> GetIpInfoAsync(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("IP address cannot be null or empty", nameof(ip));

            try
            {
                var url = $"{_baseUrl}?apiKey={_apiKey}&ip={ip}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IpInfo>(json);
            }
            catch
            {
                return null;
            }
        }
    }

    public class GeoSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
    }
}
