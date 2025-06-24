using FarmaControl_App.Views;

namespace FarmaControl_App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            CargarLogin();
            //CargarCashier();
        }

        public void CargarLogin()
        {
            Vista.Content = new LoginModule(this);
        }

        public void CargarAdmin()
        {
            Vista.Content = new AdminModule();
        }

        public void CargarCashier()
        {
            Vista.Content = new CashierModule();
        }
    }
}
