using Microsoft.Extensions.Logging;
using Listify;
using Listify.Data;
using Listify.ViewModels;
using Listify.Views;

namespace Listify;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Ruta de la base de datos SQLite
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "listify.db3");

        // Registro de dependencias (DI)
        builder.Services.AddSingleton(new ComprasDatabase(dbPath));
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
