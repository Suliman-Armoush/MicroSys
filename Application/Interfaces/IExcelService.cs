using Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IExcelService
    {
        byte[] GenerateMikrotikReport(
    List<DepartmentConsumptionResponse> serviceData,
    List<DepartmentConsumptionResponse> tcShopsData,
    List<DepartmentConsumptionResponse> shopsData);
        byte[] GenerateDetailedExcelReport(
         List<DetailedDepartmentConsumptionResponse> serviceData,
         List<DetailedDepartmentConsumptionResponse> tcShopsData,
         List<DetailedDepartmentConsumptionResponse> shopsData);
    }
}
