using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Request
{
    // كلاس التحديث - لازم يحتوي على كل شي ممكن يتغير
    public class UpdateMikrotikUserRequest
    {
        public string? NewUsername { get; set; }
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string? Server { get; set; }
        public string Comment { get; set; } 
        public long? LimitBytes { get; set; }
    }
}
