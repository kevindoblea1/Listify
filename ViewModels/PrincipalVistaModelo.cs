using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;

namespace Listify.ViewModels
{
    public partial class PrincipalVistaModelo : ObservableObject
    {
        private readonly BaseDeDatosServicio _baseDeDatos;

        [ObservableProperty]
        private string textoBusqueda = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Articulo> articulos = new();

        public PrincipalVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
            _ = CargarArticulosAsync();
        }

        [RelayCommand]
        private async Task CargarArticulosAsync()
        {
            var lista = await _baseDeDatos.ObtenerArticulosAsync();
            Articulos = new ObservableCollection<Articulo>(lista);
        }

        [RelayCommand]
        private async Task BuscarArticulosAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                await CargarArticulosAsync();
                return;
            }

            var resultados = await _baseDeDatos.BuscarArticulosAsync(texto);
            Articulos = new ObservableCollection<Articulo>(resultados);
        }

        [RelayCommand]
        private async Task AgregarArticuloAsync()
        {
            await Shell.Current.GoToAsync("EditarArticuloPagina");
        }

        [RelayCommand]
        private async Task EditarArticuloAsync(Articulo articulo)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Articulo", articulo }
            };

            await Shell.Current.GoToAsync("EditarArticuloPagina", parametros);
        }

        [RelayCommand]
        private async Task EliminarArticuloAsync(Articulo articulo)
        {
            bool confirmar = await App.Current.MainPage.DisplayAlert(
                "Eliminar",
                $"¿Eliminar '{articulo.Nombre}'?",
                "Sí", "No");

            if (confirmar)
            {
                await _baseDeDatos.EliminarArticuloAsync(articulo);
                await CargarArticulosAsync();
            }
        }

        [RelayCommand]
        private async Task AlternarCompradoAsync(Articulo articulo)
        {
            articulo.Comprado = !articulo.Comprado;
            await _baseDeDatos.ActualizarArticuloAsync(articulo);
            await CargarArticulosAsync();
        }
    }
}

