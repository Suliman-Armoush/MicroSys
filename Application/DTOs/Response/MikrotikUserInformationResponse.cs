using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class MikrotikUserInformationResponse
    {
        public string Username { get; set; }
        public string Profile { get; set; }
        public string Server { get; set; }
        public string Comment { get; set; }
        public double? LimitGB { get; set; }
        public bool IsDisabled { get; set; }
    }
}
