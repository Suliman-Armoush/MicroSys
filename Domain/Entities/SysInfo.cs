using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public class SysInfo
  {
    public int Id { get; set; }
    public string MikroTikIp { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public double DvrPrice { get; set; }

    public int segment1 { get; set; } = 1500;
    public int segment2 { get; set; } = 1400;
    public int segment3 { get; set; } = 1300;
    public int segment4 { get; set; } = 1200;
    public int segment5 { get; set; } = 1100;
    public int segment6 { get; set; } = 1000;

  }
}
