using FarmaControl_UI.ViewModels;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.Views;

public partial class LoginModule : ContentPage
{
    public LoginModule()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}
