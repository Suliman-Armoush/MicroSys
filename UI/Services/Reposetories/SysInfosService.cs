using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
  public class SysInfosService : ISysInfosService
  {
    private readonly HttpClient _httpClient;

    public SysInfosService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    public async Task<List<SysInfosResponseDto>> GetAsync()
    {
      return await _httpClient.GetFromJsonAsync<List<SysInfosResponseDto>>("api/SysInfo")
             ?? new List<SysInfosResponseDto>();
    }
    public async Task UpdateAsync(int id, SysInfosRequestDto dto)
    {
      var response = await _httpClient.PutAsJsonAsync($"api/SysInfo/{id}", dto);
      response.EnsureSuccessStatusCode();
    }
  }
}
