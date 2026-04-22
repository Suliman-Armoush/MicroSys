using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class DetailedDepartmentConsumptionResponse
    {
        public string DepartmentName { get; set; } = null!;
        public double TotalConsumptionGB { get; set; }

        public List<UserConsumptionDetail> Users { get; set; } = new();
    }

    public class UserConsumptionDetail
    {
        public string UserName { get; set; } = null!;
        public double UsageGB { get; set; }
    }
}

