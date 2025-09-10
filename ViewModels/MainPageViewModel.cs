using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Listify.Data;
using Listify.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Listify.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ComprasDatabase _database;

        public ObservableCollection<Compra> Compras { get; set; } = new();

        private string _textoBusqueda;
        public string TextoBusqueda
        {
            get => _textoBusqueda;
            set
            {
                _textoBusqueda = value;
                OnPropertyChanged(nameof(TextoBusqueda));
                BuscarCommand.Execute(null);
            }
        }

        public string NombreNuevaCompra { get; set; }

        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand ToggleCompradoCommand { get; }
        public ICommand BuscarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel(ComprasDatabase database)
        {
            _database = database;

            AgregarCommand = new Command(async () => await AgregarCompra());
            EliminarCommand = new Command<Compra>(async (compra) => await EliminarCompra(compra));
            ToggleCompradoCommand = new Command<Compra>(async (compra) => await ToggleComprado(compra));
            BuscarCommand = new Command(async () => await BuscarCompras());

            CargarCompras();
        }

        private async void CargarCompras()
        {
            var compras = await _database.GetComprasAsync();
            Compras.Clear();
            foreach (var compra in compras)
                Compras.Add(compra);
        }

        private async Task AgregarCompra()
        {
            if (!string.IsNullOrWhiteSpace(NombreNuevaCompra))
            {
                var nueva = new Compra { Nombre = NombreNuevaCompra, EstaComprado = false };
                await _database.SaveCompraAsync(nueva);
                NombreNuevaCompra = string.Empty;
                CargarCompras();
                OnPropertyChanged(nameof(NombreNuevaCompra));
            }
        }

        private async Task EliminarCompra(Compra compra)
        {
            await _database.DeleteCompraAsync(compra);
            CargarCompras();
        }

        private async Task ToggleComprado(Compra compra)
        {
            compra.EstaComprado = !compra.EstaComprado;
            await _database.SaveCompraAsync(compra);
            CargarCompras();
        }

        private async Task BuscarCompras()
        {
            var resultados = string.IsNullOrWhiteSpace(TextoBusqueda)
                ? await _database.GetComprasAsync()
                : await _database.BuscarComprasAsync(TextoBusqueda);

            Compras.Clear();
            foreach (var c in resultados)
                Compras.Add(c);
        }

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
