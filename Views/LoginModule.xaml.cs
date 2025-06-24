using FarmaControl_App.ViewModels;
namespace FarmaControl_App.Views;

public partial class LoginModule : ContentView
{
	public LoginModule()
	{
		InitializeComponent();
        viewModel = new LoginViewModel();
    }

    private async void loginClicked(object sender, EventArgs e)
    {
        string correo = email.Text;
        string password = contrasenia.Text;

        bool loginExitoso = await viewModel.VerificarLogin(correo, password);

        if (loginExitoso)
        {
            await DisplayAlert("Éxito", "Acceso concedido", "OK");
            await Shell.Current.GoToAsync("//AdminModule"); // O la vista que corresponda
        }
        else
        {
            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }
}