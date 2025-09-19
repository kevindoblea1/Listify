using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Listify.Modelos;
using Listify.Servicios;
using System.Windows.Input;

namespace Listify.ViewModels
{
    [QueryProperty(nameof(Articulo), "Articulo")]
    public partial class EditarArticuloVistaModelo : ObservableObject
    {
        private readonly BaseDeDatosServicio _baseDeDatos;

        [ObservableProperty]
        private Articulo articulo = new();

        // Asegúrate de que la clase sea pública y tenga un constructor sin parámetros público
        public EditarArticuloVistaModelo(BaseDeDatosServicio baseDeDatos)
        {
            _baseDeDatos = baseDeDatos;
        }

        public EditarArticuloVistaModelo()
        {
            // Constructor sin parámetros
        }

        [RelayCommand]
        private async Task GuardarAsync()
        {
            if (string.IsNullOrWhiteSpace(Articulo.Nombre))
            {
                await App.Current.MainPage.DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            if (Articulo.Id == 0)
                await _baseDeDatos.AgregarArticuloAsync(Articulo);
            else
                await _baseDeDatos.ActualizarArticuloAsync(Articulo);

            await Shell.Current.GoToAsync(".."); // Volver atrás
        }

        [RelayCommand]
        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync(".."); // Volver atrás
        }
    }
}
