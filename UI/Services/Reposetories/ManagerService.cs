using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
    public class ManagerService : IManagerService
    {
        private readonly HttpClient _httpClient;

        public ManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DetailedDepartmentConsumptionResponseDto?> GetMyDepartmentUsageAsync()
        {
            return await _httpClient.GetFromJsonAsync<DetailedDepartmentConsumptionResponseDto>("api/Report/MyDepartment/Usage");
        }

        public async Task<List<MikrotikUserResponseDto>> GetUsersByDepartmentAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>("api/Manager/UsersByDepartment") ?? new();
        }

        public async Task<MikrotikUserInformationResponseDto?> AddUserAsync(ManagerMikrotikUserRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Manager/Add/User", request);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<MikrotikUserInformationResponseDto>()
                : null;
        }
    }
}
