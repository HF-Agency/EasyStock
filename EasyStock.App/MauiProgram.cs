using EasyStock.App.Services.Clients;
using EasyStock.Library.Entities.Interface;
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

            // Initialize NLog
            builder.Logging.AddNLog();

            // Register Services
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7260") });
            builder.Services.AddSingleton<IProductClient, ProductClient>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
