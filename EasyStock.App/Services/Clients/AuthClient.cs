using EasyStock.App.Services.Interfaces;
using EasyStock.Library.Entities.Authentication;
using System.Net.Http.Json;

namespace EasyStock.App.Services.Clients
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse?> LoginUserAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/login", model);
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<AuthResponse?> RegisterUserAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/register", model);
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<AuthResponse?> LogoutUserAsync()
        {
            var response = await _httpClient.PostAsync("Auth/logout", null);
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }
    }
}
