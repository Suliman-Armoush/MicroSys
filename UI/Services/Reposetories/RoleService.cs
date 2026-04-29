using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoleResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RoleResponseDto>>("api/Role/GetAll")
                   ?? new List<RoleResponseDto>();
        }

        public async Task<RoleResponseDto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<RoleResponseDto>($"api/Role/GetById/{id}");
        }

        public async Task<RoleResponseDto> CreateAsync(RoleRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Role/Create", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RoleResponseDto>();
        }

        public async Task<RoleResponseDto> UpdateAsync(int id, RoleRequestDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Role/Update/{id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RoleResponseDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Role/Delete/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
