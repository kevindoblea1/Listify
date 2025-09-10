using Listify.Models;
using Listify.ViewModels;

namespace Listify.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Compra compra)
        {
            if (BindingContext is MainPageViewModel vm)
            {
                vm.ToggleCompradoCommand.Execute(compra);
            }
        }
    }

    // Esto es opcional, pero útil si el BindingContext aún no está listo en el CheckedChanged
    private void CheckBox_BindingContextChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            checkBox.BindingContextChanged -= CheckBox_BindingContextChanged;
        }
    }
}
