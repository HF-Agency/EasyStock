using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EasyStock.App.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public ApiAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await SecureStorage.GetAsync("authToken");
            var identity = new ClaimsIdentity();

            if (token != null)
            {
                // Optionally verify the token and extract claims
                // For simplicity, we're just creating a basic identity here
                identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, "User"),
                // Add more claims if necessary
            }, "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string userName)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }, "jwt"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }

}
