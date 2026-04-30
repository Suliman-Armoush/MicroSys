using System.Text.Json.Serialization;
using UI.Models.Enums;

namespace UI.Models.Request
{
    public class DepartmentRequestDto
    {
        public string Name { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DepartmentTypes Type { get; set; }

        public int DvrNum { get; set; }
    }
}
