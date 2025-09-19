using Microsoft.Extensions.DependencyInjection;

namespace Listify.Servicios
{
    public static class ServicioLocalizador
    {
        public static IServiceProvider Servicios { get; set; } = default!;

        public static T Obtener<T>() where T : notnull
            => Servicios.GetRequiredService<T>();
    }
}
