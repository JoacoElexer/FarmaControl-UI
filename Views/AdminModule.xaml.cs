using FarmaControl_App.ViewModels; // Aseg�rate de agregar esta importaci�n

namespace FarmaControl_App.Views
{
    public partial class AdminModule : ContentView
    {
        private MainPage mainPage;
        public AdminModule(MainPage page)
        {
            InitializeComponent();
            mainPage = page;
            // Establece el BindingContext de la vista a una instancia de tu ViewModel
            // Esto permite que los elementos de la UI (botones) se enlacen a los comandos del ViewModel
            // this.BindingContext = new AdminModuleViewModel();
        }

        private async void usersClicked(object sender, EventArgs e)
        {
            mainPage.CargarUsuarios();
        }
    }
}
