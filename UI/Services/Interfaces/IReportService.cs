using UI.Models.Response;

namespace UI.Services.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> DownloadDepartmentConsumptionExcelAsync();
        Task<byte[]> DownloadDetailedConsumptionExcelAsync();
        Task<List<DepartmentConsumptionResponseDto>> GetDepartmentsSummaryAsync();
    }
}
