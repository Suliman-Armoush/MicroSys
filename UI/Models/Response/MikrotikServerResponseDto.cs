namespace UI.Models.Response
{
    public class MikrotikServerResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Interface { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
    }
}
