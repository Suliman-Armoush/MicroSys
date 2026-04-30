namespace UI.Models.Response
{
    public class MikrotikUserResponseDto
    {
        public string Comment { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        // تأكد أن الأسماء مطابقة تماماً لما يرسله الـ API
        public double BytesIn { get; set; }
        public double BytesOut { get; set; }
        public double Total { get; set; }

        
    }
}
