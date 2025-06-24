using FarmaControl_App.ViewModel;
namespace FarmaControl_App.Views;

public partial class LoginModule : ContentView
{
    private readonly LoginModuleViewModel viewModel = new LoginModuleViewModel();
    public LoginModule()
    {
        InitializeComponent();
    }

    private async void loginClicked(object sender, EventArgs e)
    {
        string correo = email.Text;
        string password = contrasenia.Text; // Ensure 'contrasenia' is defined in the XAML file  

        bool loginExitoso = await viewModel.VerificarLogin(correo, password);

        if (loginExitoso)
        {
            //await DisplayAlert("Éxito", "Acceso concedido", "OK");
            await Shell.Current.GoToAsync("//AdminModule");
        }
        else
        {
            //await DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }
}