using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MikrotikConsumption
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long BytesIn { get; set; }  
        public long BytesOut { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
