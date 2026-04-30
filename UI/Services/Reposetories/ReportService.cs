using System.Net.Http.Json;
using UI.Services.Interfaces;
using UI.Models.Response;
namespace UI.Services.Reposetories
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> DownloadDepartmentConsumptionExcelAsync()
        {
            var response = await _httpClient.GetAsync("api/Report/Export/Departments/Consumption");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            return null;
        }

        public async Task<byte[]> DownloadDetailedConsumptionExcelAsync()
        {
            var response = await _httpClient.GetAsync("api/Report/Export/Detailed/Consumption");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Detailed Report Error: {response.StatusCode} - {errorContent}");
                return null;
            }
        }

        public async Task<List<DepartmentConsumptionResponseDto>> GetDepartmentsSummaryAsync()
        {
            try
            {
                // المسار api/Report/Departments/Consumption حسب الـ Controller عندك    
                var result = await _httpClient.GetFromJsonAsync<List<DepartmentConsumptionResponseDto>>("api/Report/Departments/Consumption");
                return result ?? new List<DepartmentConsumptionResponseDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching summary: {ex.Message}");
                return new List<DepartmentConsumptionResponseDto>();
            }
        }
    }
}
