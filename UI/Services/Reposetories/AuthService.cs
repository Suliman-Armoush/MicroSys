using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using UI.Models.Request;
using UI.Models.Response;
using UI.Services.Interfaces;

namespace UI.Services.Reposetories
{



  public class AuthService : IAuthService
  {
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
      _httpClient = httpClient;
      _localStorage = localStorage;
    }

    private const string TokenKey = "authToken";
    private const string UserNameKey = "userName";
    private const string RoleKey = "role";

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
      var response = await _httpClient.PostAsJsonAsync("api/Auth/login", request); // غيّر المسار حسب API الخاص بك

      if (response.IsSuccessStatusCode)
      {
        var authResult = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResult != null && authResult.Success)
        {
          await _localStorage.SetItemAsync(TokenKey, authResult.Token);
          await _localStorage.SetItemAsync(UserNameKey, authResult.UserName);
          await _localStorage.SetItemAsync(RoleKey, authResult.Role);
          return authResult;
        }
      }

      // محاولة قراءة خطأ مخصص من الـ API (قد يعيد 401 مع JSON)
      var error = await response.Content.ReadFromJsonAsync<AuthResponse>();
      return error ?? new AuthResponse { Success = false, Message = "Login failed" };
    }

    public async Task LogoutAsync()
    {
      var response = await _httpClient.PostAsync("api/Auth/logout", null);

      if (response.IsSuccessStatusCode)
      {
        await _localStorage.RemoveItemAsync(TokenKey);
        await _localStorage.RemoveItemAsync(UserNameKey);
        await _localStorage.RemoveItemAsync(RoleKey);
      }
    }


    public async Task<string?> GetTokenAsync() => await _localStorage.GetItemAsync<string>(TokenKey);

    public async Task<bool> IsAuthenticatedAsync() => !string.IsNullOrEmpty(await GetTokenAsync());
  }
}

