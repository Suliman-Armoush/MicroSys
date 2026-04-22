using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class DepartmentConsumptionResponse
    {
        public string DepartmentName { get; set; } = null!;
  
        public double TotalConsumptionGB { get; set; }
        public int ActiveUsersCount { get; set; }
    }

    
}
