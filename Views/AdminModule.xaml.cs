using FarmaControl_UI.ViewModels;

namespace FarmaControl_UI.Views;

public partial class AdminModule : ContentView
{
    public AdminModule()
    {
        InitializeComponent();
        BindingContext = new AdminViewModel();
    }
}
