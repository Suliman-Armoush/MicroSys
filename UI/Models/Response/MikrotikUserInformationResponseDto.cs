namespace UI.Models.Response
{
    public class MikrotikUserInformationResponseDto
    {
        public string Username { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Server { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public double? LimitGB { get; set; }
        public bool IsDisabled { get; set; }
    }
}
