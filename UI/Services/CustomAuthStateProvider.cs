using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;

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
            if (string.IsNullOrEmpty(token) || IsTokenExpired(token))
            {
                // إزالة التوكن من الترويسة لتجنب إرساله مع طلبات لاحقة
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var expClaim = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                if (expClaim != null && long.TryParse(expClaim, out long expSeconds))
                {
                    var expiry = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
                    return expiry <= DateTimeOffset.UtcNow;
                }
                return true; // إذا لم يوجد claim exp أو كان غير صالح
            }
            catch
            {
                return true; // أي خطأ في فك التوكن يعني أنه غير صالح
            }
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
