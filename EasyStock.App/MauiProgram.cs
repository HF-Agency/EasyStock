using EasyStock.Library.Services.Clients;
using Microsoft.Extensions.Logging;

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

            // Register HttpClient
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7260") });
            // Register ProductClient
            builder.Services.AddSingleton<ProductClient>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
