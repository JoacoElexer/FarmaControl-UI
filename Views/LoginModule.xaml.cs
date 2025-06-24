using FarmaControl_UI.ViewModels;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.Views;

public partial class LoginModule : ContentView
{
    public LoginModule()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}
