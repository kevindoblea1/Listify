using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Listify.Modelos;

namespace Listify.Servicios
{
    public class BaseDeDatosServicio
    {
        private SQLiteAsyncConnection _conexion;

        private async Task CrearConexionAsync()
        {
            if (_conexion != null)
                return;

            string rutaBD = Path.Combine(FileSystem.AppDataDirectory, "compras.db3");
            _conexion = new SQLiteAsyncConnection(rutaBD);
            await _conexion.CreateTableAsync<Articulo>();
        }

        public async Task InicializarAsync()
        {
            await CrearConexionAsync();
        }

        public async Task<List<Articulo>> ObtenerArticulosAsync()
        {
            await CrearConexionAsync();
            return await _conexion.Table<Articulo>().OrderBy(a => a.Nombre).ToListAsync();
        }

        public async Task<List<Articulo>> BuscarArticulosAsync(string texto)
        {
            await CrearConexionAsync();
            return await _conexion.Table<Articulo>()
                .Where(a => a.Nombre.ToLower().Contains(texto.ToLower()))
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        public async Task AgregarArticuloAsync(Articulo articulo)
        {
            await CrearConexionAsync();
            await _conexion.InsertAsync(articulo);
        }

        public async Task ActualizarArticuloAsync(Articulo articulo)
        {
            await CrearConexionAsync();
            await _conexion.UpdateAsync(articulo);
        }

        public async Task EliminarArticuloAsync(Articulo articulo)
        {
            await CrearConexionAsync();
            await _conexion.DeleteAsync(articulo);
        }
    }
}
