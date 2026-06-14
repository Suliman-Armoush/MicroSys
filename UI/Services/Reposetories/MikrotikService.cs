using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
    public class MikrotikService : IMikrotikService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MikrotikService> _logger;

        public MikrotikService(HttpClient httpClient, ILogger<MikrotikService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // دالة مساعدة لجلب محتوى الخطأ من الـ API
        private async Task<string> GetResponseError(HttpResponseMessage response)
        {
            try { return await response.Content.ReadAsStringAsync(); }
            catch { return "An unexpected error occurred."; }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try { return (await _httpClient.GetAsync("api/Mikrotik/TestConnection")).IsSuccessStatusCode; }
            catch (Exception ex) { _logger.LogError(ex, "Error in TestConnection"); return false; }
        }

        public async Task<List<MikrotikUserResponseDto>> GetAllUsersAsync()
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>("api/mikrotik/users") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in GetAllUsers"); return new(); }
        }

        public async Task<List<MikrotikProfileResponseDto>> GetAllProfilesAsync()
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikProfileResponseDto>>("api/Mikrotik/Profiles") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in GetAllProfiles"); return new(); }
        }

        public async Task<List<MikrotikServerResponseDto>> GetServersListAsync()
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikServerResponseDto>>("api/Mikrotik/Get/Servers") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in GetServersList"); return new(); }
        }

        public async Task<MikrotikUserInformationResponseDto?> CreateUserAsync(CreateMikrotikUserRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Mikrotik/Create", request);
                if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<MikrotikUserInformationResponseDto>();

                // هنا التعديل: رمي استثناء يحتوي على رسالة الخطأ ليتم التقاطها في الـ Component
                throw new Exception(await GetResponseError(response));
            }
            catch (Exception ex) { _logger.LogError(ex, "Error in CreateUser"); throw; } // إعادة رمي الاستثناء ليظهر في المودال
        }

        public async Task<MikrotikUserInformationResponseDto?> GetUserByNameAsync(string username)
        {
            try { return await _httpClient.GetFromJsonAsync<MikrotikUserInformationResponseDto>($"api/Mikrotik/Get/User/{username}"); }
            catch (Exception ex) { _logger.LogError(ex, "Error in GetUserByName"); return null; }
        }

        public async Task<MikrotikUserInformationResponseDto?> UpdateUserAsync(string currentUsername, UpdateMikrotikUserRequestDto model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Mikrotik/update/{currentUsername}", model);
                if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<MikrotikUserInformationResponseDto>();
                throw new Exception(await GetResponseError(response));
            }
            catch (Exception ex) { _logger.LogError(ex, "Error in UpdateUser"); throw; }
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            try { return (await _httpClient.DeleteAsync($"api/Mikrotik/Delete/{username}")).IsSuccessStatusCode; }
            catch (Exception ex) { _logger.LogError(ex, "Error in DeleteUser"); return false; }
        }

        public async Task<string?> DisableUserAsync(string username)
        {
            try
            {
                var response = await _httpClient.PutAsync($"api/mikrotik/{username}/Disable", null);
                return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) { _logger.LogError(ex, "Error in DisableUser"); return "Connection Error"; }
        }

        public async Task<string?> EnableUserAsync(string username)
        {
            try
            {
                var response = await _httpClient.PutAsync($"api/mikrotik/{username}/Enable", null);
                return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) { _logger.LogError(ex, "Error in EnableUser"); return "Connection Error"; }
        }

        public async Task<List<MikrotikUserResponseDto>> SearchUsersAsync(string term)
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>($"api/Mikrotik/Search?term={term}") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in SearchUsers"); return new(); }
        }

        public async Task<List<MikrotikHostResponse>> GetAllHostsAsync()
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikHostResponse>>("api/Host/GetAll") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in GetAllHosts"); return new(); }
        }

        public async Task<List<MikrotikHostResponse>> SearchHostsAsync(string term)
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikHostResponse>>($"api/Host/Search?term={term}") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in SearchHosts"); return new(); }
        }

        public async Task<bool> RemoveHostAsync(string macAddress)
        {
            try { return (await _httpClient.DeleteAsync($"api/Host/Delete/{Uri.EscapeDataString(macAddress)}")).IsSuccessStatusCode; }
            catch (Exception ex) { _logger.LogError(ex, "Error in RemoveHost"); return false; }
        }

        public async Task<bool> RemoveAllHostsAsync()
        {
            try { return (await _httpClient.DeleteAsync("api/Host/Delete/All")).IsSuccessStatusCode; }
            catch (Exception ex) { _logger.LogError(ex, "Error in RemoveAllHosts"); return false; }
        }

        public async Task<bool> ResetAllCountersAsync()
        {
            try { return (await _httpClient.DeleteAsync("api/Mikrotik/ResetAllCounters")).IsSuccessStatusCode; }
            catch (Exception ex) { _logger.LogError(ex, "Error in ResetAllCounters"); return false; }
        }

        public async Task<List<MikrotikUserResponseDto>> GetUsersByDepartmentAsync()
        {
            try { return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>("api/Manager/UsersByDepartment") ?? new(); }
            catch (Exception ex) { _logger.LogError(ex, "Error in GetUsersByDepartment"); return new(); }
        }
    }
}