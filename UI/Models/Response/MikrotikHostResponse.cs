using System.Text.Json.Serialization;

namespace UI.Models.Response
{
    public class MikrotikHostResponse
    {
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = "";

        [JsonPropertyName("macAddress")]
        public string MacAddress { get; set; } = "";

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; } = "";

        [JsonPropertyName("uptime")]
        public string Uptime { get; set; } = "";
    }
}
