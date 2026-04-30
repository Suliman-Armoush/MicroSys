namespace UI.Models.Response
{
    public class MikrotikProfileResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string? SharedUsers { get; set; }
        public string? RateLimit { get; set; }
    }
}
