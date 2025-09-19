using Listify.Vistas;

namespace Listify;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("EditarArticuloPagina", typeof(EditarArticuloPagina));
    }
}
