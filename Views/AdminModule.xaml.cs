using FarmaControl_UI.ViewModels; 

namespace FarmaControl_UI.Views;

public partial class AdminModule : ContentView
{
    public AdminModule()
    {
        InitializeComponent();
        // Asigna tu ViewModel como el BindingContext de este ContentView
        BindingContext = new AdminViewModel();
    }
}