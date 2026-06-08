using System.Text.Json.Serialization;

namespace UI.Models.Response
{
  public class MikrotikUserResponseDto
  {
    public string Comment { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public double BytesIn { get; set; }
    public double BytesOut { get; set; }
    public double Total { get; set; }
    public string Profile { get; set; }
    public double LimitTotal { get; set; }
  }
}

