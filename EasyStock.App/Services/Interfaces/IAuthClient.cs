using EasyStock.Library.Entities.Authentication;

namespace EasyStock.App.Services.Interfaces
{
    public interface IAuthClient
    {
        Task<AuthResponse?> LoginUserAsync(LoginModel model);
        Task<AuthResponse?> RegisterUserAsync(RegisterModel model);
        Task<AuthResponse?> LogoutUserAsync();
    }
}
