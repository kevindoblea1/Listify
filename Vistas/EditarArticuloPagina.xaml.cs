using Listify.Servicios;
using Listify.ViewModels;

namespace Listify.Vistas;

public partial class EditarArticuloPagina : ContentPage
{
    public EditarArticuloPagina()
    {
        InitializeComponent();
        BindingContext = ServicioLocalizador.Obtener<EditarArticuloVistaModelo>();
    }
}
