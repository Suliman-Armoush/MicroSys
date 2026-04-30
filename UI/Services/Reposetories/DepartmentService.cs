using System.Net.Http.Json;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<DepartmentResponseDto>> GetAllAsync()
        {
            // تأكد أن المسار هو api/Department/GetAll وليس api/Departments
            return await _httpClient.GetFromJsonAsync<List<DepartmentResponseDto>>("api/Department/GetAll") ?? new();
        }
    }
}