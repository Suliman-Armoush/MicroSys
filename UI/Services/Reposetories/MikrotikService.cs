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

    public async Task<bool> TestConnectionAsync()
    {
      var response = await _httpClient.GetAsync("api/Mikrotik/TestConnection");

      if (!response.IsSuccessStatusCode)
        return false;

      var result = await response.Content.ReadFromJsonAsync<TestConnectionResponseDto>();

      return result?.Success == true;
    }

    public async Task<List<MikrotikUserResponseDto>> GetAllUsersAsync()
    {
      return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>("api/mikrotik/users") ?? new();
    }

    public async Task<List<MikrotikProfileResponseDto>> GetAllProfilesAsync()
    {
      return await _httpClient.GetFromJsonAsync<List<MikrotikProfileResponseDto>>("api/Mikrotik/Profiles") ?? new();
    }

    public async Task<List<MikrotikServerResponseDto>> GetServersListAsync()
    {
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

    public async Task<MikrotikUserInformationResponseDto?> GetUserByNameAsync(string username)
    {
      return await _httpClient.GetFromJsonAsync<MikrotikUserInformationResponseDto>($"api/Mikrotik/Get/User/{username}");
    }

    public async Task<MikrotikUserInformationResponseDto?> UpdateUserAsync(string currentUsername, UpdateMikrotikUserRequestDto model)
    {
      var response = await _httpClient.PutAsJsonAsync($"api/Mikrotik/update/{currentUsername}", model);
      return response.IsSuccessStatusCode
          ? await response.Content.ReadFromJsonAsync<MikrotikUserInformationResponseDto>()
          : null;
    }

    public async Task<bool> DeleteUserAsync(string username)
    {
      var response = await _httpClient.DeleteAsync($"api/Mikrotik/Delete/{username}");
      return response.IsSuccessStatusCode;
    }

    // في ملف UI/Services/Repositories/MikrotikService.cs

    public async Task<string?> DisableUserAsync(string username)
    {
      var response = await _httpClient.PutAsync($"api/mikrotik/{username}/Disable", null);

      if (!response.IsSuccessStatusCode)
      {
        // قراءة الرسالة القادمة من الباك إند (مثل: This user is already disabled)
        return await response.Content.ReadAsStringAsync();
      }

      return null; // نجاح
    }

    public async Task<string?> EnableUserAsync(string username)
    {
      var response = await _httpClient.PutAsync($"api/mikrotik/{username}/Enable", null);

      if (!response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();
      }

      return null; // نجاح
    }

    public async Task<List<MikrotikUserResponseDto>> SearchUsersAsync(string term)
    {
      return await _httpClient.GetFromJsonAsync<List<MikrotikUserResponseDto>>($"api/Mikrotik/Search?term={term}") ?? new();
    }


    public async Task<List<MikrotikHostResponse>> GetAllHostsAsync() =>
        await _httpClient.GetFromJsonAsync<List<MikrotikHostResponse>>("api/Host/GetAll") ?? new();

    public async Task<List<MikrotikHostResponse>> SearchHostsAsync(string term)
    {
      var result = await _httpClient.GetFromJsonAsync<List<MikrotikHostResponse>>($"api/Host/Search?term={term}");
      return result ?? new List<MikrotikHostResponse>();
    }

    public async Task<bool> RemoveHostAsync(string macAddress)
    {
      var encodedMac = Uri.EscapeDataString(macAddress);
      var response = await _httpClient.DeleteAsync($"api/Host/Delete/{encodedMac}");

      return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveAllHostsAsync()
    {
      var response = await _httpClient.DeleteAsync("api/Host/Delete/All");
      return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetAllCountersAsync()
    {
      var response = await _httpClient.DeleteAsync("api/Mikrotik/ResetAllCounters");
      return response.IsSuccessStatusCode;
    }

  }
}

