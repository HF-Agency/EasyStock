using EasyStock.App.Services;
using EasyStock.App.Services.Clients;
using EasyStock.App.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Radzen;

namespace EasyStock.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            

            // Add Radzen Components
            builder.Services.AddRadzenComponents();
            builder.Services.AddAuthorizationCore();

            // Initialize NLog
            builder.Logging.AddNLog();

            // Register Services
            //builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7260") });
            builder.Services.AddScoped<JwtMessageHandler>();
            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7260");
            })
            .AddHttpMessageHandler<JwtMessageHandler>();

            builder.Services.AddSingleton<ProductClient>();
            builder.Services.AddSingleton<AuthClient>();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();


            // Ensure HttpClient instances are correctly resolved
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
