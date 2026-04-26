using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Request
{
    public class CreateMikrotikUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public string Server { get; set; }
        public string Comment { get; set; }
        public long? LimitBytes { get; set; }
    }
}
