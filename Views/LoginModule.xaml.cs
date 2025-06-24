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

        var usuario = await viewModel.VerificarLogin(correo, password);

        if(usuario != null)
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", $"Bienvenido {usuario.nombre_Completo}", "OK");
            
            switch (usuario.rol.ToLower())
            {
                case "administrador":
                    mainPage.CargarAdmin();
                    break;
                case "cajero":
                    mainPage.CargarCashier();
                    break;
                case "farmacéutico":
                    mainPage.CargarFarmaceutic();
                    break;
                default:
                    await Application.Current.MainPage.DisplayAlert("Error", "Rol no reconocido", "OK");
                    break;
            }
        }else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }
}