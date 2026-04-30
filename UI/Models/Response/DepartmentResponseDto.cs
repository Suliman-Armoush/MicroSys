using System.Text.Json.Serialization;

namespace UI.Models.Response
{
    public class DepartmentResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;   // <- string, NOT int

        [JsonPropertyName("dvrNum")]
        public int DvrNum { get; set; }
    }
}