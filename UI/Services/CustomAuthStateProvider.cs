using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace UI.Services
{
    

   
        public class CustomAuthStateProvider : AuthenticationStateProvider
        {
            private readonly ILocalStorageService _localStorage;
            private readonly HttpClient _httpClient;

            public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
            {
                _localStorage = localStorage;
                _httpClient = httpClient;
            }

            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (string.IsNullOrEmpty(token))
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                // Attach the token to the HttpClient header for subsequent requests.
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }

            public void NotifyUserAuthentication(string token)
            {
                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
                var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
                NotifyAuthenticationStateChanged(authState);
            }

            public void NotifyUserLogout()
            {
                var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                var authState = Task.FromResult(new AuthenticationState(anonymousUser));
                NotifyAuthenticationStateChanged(authState);
            }

            private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                return token.Claims;
            }
        }
    
}
