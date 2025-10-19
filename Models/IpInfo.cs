using Newtonsoft.Json;

namespace GeoBlockAPI.Models
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; } = string.Empty;

        [JsonProperty("country_code2")]
        public string CountryCode { get; set; } = string.Empty;

        [JsonProperty("country_name")]
        public string CountryName { get; set; } = string.Empty;

        [JsonProperty("isp")]
        public string Isp { get; set; } = string.Empty;
    }
}
