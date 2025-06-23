using FarmaControl_UI.ViewModels;

namespace FarmaControl_UI.Views;

public partial class LoginModule : ContentView
{
    public LoginModule()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}
