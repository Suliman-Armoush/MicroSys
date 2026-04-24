using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DTOs.Response
{
    public class DepartmentConsumptionResponse
    {
        public string DepartmentName { get; set; } = null!;
  
        public double TotalConsumptionGB { get; set; }
        public int ActiveUsersCount { get; set; }
        [JsonIgnore]
        public string Type { get; set; }


    }

    
}
