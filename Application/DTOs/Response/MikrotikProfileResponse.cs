using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class MikrotikProfileResponse
    {
        public string Name { get; set; } = null!;
        public string? SharedUsers { get; set; } 
        public string? RateLimit { get; set; }  
    }
}
