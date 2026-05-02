using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
    public class DepartmentService : IDepartmentService
    {
        //private readonly HttpClient _httpClient;
        //public DepartmentService(HttpClient httpClient) => _httpClient = httpClient;

        //public async Task<List<DepartmentResponseDto>> GetAllAsync()
        //{
        //    // Ensure the route is api/Department/GetAll, not api/Departments.
        //    return await _httpClient.GetFromJsonAsync<List<DepartmentResponseDto>>("api/Department/GetAll") ?? new();
        //}




        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DepartmentResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmentResponseDto>>("api/Department/GetAll")
                   ?? new List<DepartmentResponseDto>();
        }

        public async Task<DepartmentResponseDto> GetByIdAsync(int id)
        {
            // Matches the controller route: Get/{id}.
            return await _httpClient.GetFromJsonAsync<DepartmentResponseDto>($"api/Department/Get/{id}");
        }

        public async Task<DepartmentResponseDto> CreateAsync(DepartmentRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Department/Create", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DepartmentResponseDto>();
        }

        public async Task<DepartmentResponseDto> UpdateAsync(int id, DepartmentRequestDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Department/Update/{id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DepartmentResponseDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Department/Delete/{id}");
            response.EnsureSuccessStatusCode();
        }


    }
}
