namespace UI.Models.Response
{
    public class MikrotikUserResponseDto
    {
        public string Comment { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        // Property names must match the API payload.
        public double BytesIn { get; set; }
        public double BytesOut { get; set; }
        public double Total { get; set; }

        
    }
}
