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
    }
}
