using Microsoft.Extensions.Logging;
using Listify.Servicios;
using Listify.ViewModels;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<BaseDeDatosServicio>();
        builder.Services.AddSingleton<PrincipalVistaModelo>();
        builder.Services.AddTransient<EditarArticuloVistaModelo>();

        var app = builder.Build();

        ServicioLocalizador.Servicios = app.Services;

        return app;
    }
}
