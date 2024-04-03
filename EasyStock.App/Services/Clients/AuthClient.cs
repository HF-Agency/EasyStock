using EasyStock.App.Services.Interfaces;
using EasyStock.Library.Entities.Authentication;
using System.Net.Http.Json;
using Microsoft.Maui.Storage;

namespace EasyStock.App.Services.Clients
{
    public class AuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginUserAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", new { Email = model.Email, Password = model.Password });
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                if (loginResponse?.Token != null)
                {
                    await SecureStorage.SetAsync("authToken", loginResponse.Token);
                    return true;
                }
            }

            return false;
        }


        public async Task<AuthResponse?> RegisterUserAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/register", model);
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<bool> LogoutUserAsync()
        {
            return SecureStorage.Remove("authToken");
        }
    }
}
