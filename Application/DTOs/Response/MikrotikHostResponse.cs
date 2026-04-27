using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class MikrotikHostResponse
    {
        public string Comment { get; set; }

        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string Uptime { get; set; } 
    }
}
