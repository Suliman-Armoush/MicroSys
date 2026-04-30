using System.Net.Http.Json;
using UI.Models.Request;

using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
    public class MikrotikService : IMikrotikService
    {
        private readonly HttpClient _httpClient;

        public MikrotikService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MikrotikUserResponseDto>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>("api/mikrotik/users") ?? new();
        }

        public async Task<List<MikrotikProfileResponseDto>> GetAllProfilesAsync()
        {
            // المسار الصحيح حسب كود الباك إند
            return await _httpClient.GetFromJsonAsync<List<MikrotikProfileResponseDto>>("api/Mikrotik/Profiles") ?? new();
        }

        public async Task<List<MikrotikServerResponseDto>> GetServersListAsync()
        {
            // نستخدم GetFromJsonAsync مع النوع الجديد لضمان تحويل البيانات بشكل صحيح
            return await _httpClient.GetFromJsonAsync<List<MikrotikServerResponseDto>>("api/Mikrotik/Get/Servers") ?? new();
        }

        public async Task<MikrotikUserInformationResponseDto?> CreateUserAsync(CreateMikrotikUserRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Mikrotik/Create", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MikrotikUserInformationResponseDto>();
            }

            return null;
        }




    }
}

