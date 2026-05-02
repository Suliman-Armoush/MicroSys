using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{
  public class UserService : IUserService
  {

    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
      return await _httpClient.GetFromJsonAsync<List<UserResponseDto>>("api/User/GetAll")
             ?? new List<UserResponseDto>();
    }

    public async Task<UserResponseDto> GetByIdAsync(int id)
    {
      return await _httpClient.GetFromJsonAsync<UserResponseDto>($"api/User/Get/{id}");
    }

    public async Task<UserResponseDto> CreateAsync(UserRequestDto dto)
    {
      var response = await _httpClient.PostAsJsonAsync("api/User/Register", dto);
      response.EnsureSuccessStatusCode();
      return await response.Content.ReadFromJsonAsync<UserResponseDto>();
    }

    public async Task<UserResponseDto> UpdateAsync(int id, UserRequestDto dto)
    {
      var response = await _httpClient.PutAsJsonAsync($"api/User/Update/{id}", dto);
      response.EnsureSuccessStatusCode();
      return await response.Content.ReadFromJsonAsync<UserResponseDto>();
    }

    public async Task DeleteAsync(int id)
    {
      var response = await _httpClient.DeleteAsync($"api/User/Delete/{id}");
      response.EnsureSuccessStatusCode();
    }
  }
}
