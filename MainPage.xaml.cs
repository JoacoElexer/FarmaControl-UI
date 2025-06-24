using FarmaControl_App.Views;

namespace FarmaControl_App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            CargarLogin();
        }

        public void CargarLogin()
        {
            Vista.Content = new LoginModule(this);
        }

        public void CargarAdmin()
        {
            Vista.Content = new AdminModule();
        }
    }
}
