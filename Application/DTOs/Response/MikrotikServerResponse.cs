using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class MikrotikServerResponse
    {
        public string Name { get; set; }
        public string Interface { get; set; } 
        public bool IsEnabled { get; set; }  
    }
}
