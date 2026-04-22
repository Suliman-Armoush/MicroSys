using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class SysInfoResponseDto
    {
        public int Id { get; set; }
        public string? MikroTikIp { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
