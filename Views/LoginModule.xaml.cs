using FarmaControl_App.ViewModel;
namespace FarmaControl_App.Views;

public partial class LoginModule : ContentView
{
    private MainPage mainPage;
    private readonly LoginModuleViewModel viewModel = new LoginModuleViewModel();
    public LoginModule(MainPage page)
    {
        InitializeComponent();
        mainPage = page;
    }

    private async void loginClicked(object sender, EventArgs e)
    {
        string correo = email.Text;
        string password = contrasenia.Text;

        bool loginExitoso = await viewModel.VerificarLogin(correo, password);

        if (loginExitoso)
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", "Acceso concedido", "OK");
            mainPage.CargarAdmin();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }
}