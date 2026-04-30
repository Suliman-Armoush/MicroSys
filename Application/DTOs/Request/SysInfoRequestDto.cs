using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Request
{
  public class SysInfoRequestDto
  {
    public string? MikroTikIp { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public double? DvrPrice { get; set; }

    public int? segment1 { get; set; }
    public int? segment2 { get; set; }
    public int? segment3 { get; set; }
    public int? segment4 { get; set; }
    public int? segment5 { get; set; }
    public int? segment6 { get; set; }
  }
}
