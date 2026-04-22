using Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IExcelService
    {
        byte[] GenerateMikrotikReport(
            List<DepartmentConsumptionResponse> atData,
            List<DepartmentConsumptionResponse> hashData,
            List<DepartmentConsumptionResponse> normalData);
    }
}
