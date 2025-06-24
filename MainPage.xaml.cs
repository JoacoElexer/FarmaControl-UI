using FarmaControl_App.Views;

namespace FarmaControl_App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            //CargarUsuarios();
            CargarLogin();
        }


        public void CargarLogin()
        {
            Vista.Content = new LoginModule(this);
        }

        public void CargarAdmin()
        {
            Vista.Content = new AdminModule(this);
        }

        public void CargarCashier()
        {
            Vista.Content = new CashierModule();
        }

        public void CargarFarmaceutic()
        {
            Vista.Content = new FarmaceuticModule();
        }
        public void CargarUsuarios()
        {
            Vista.Content = new UserModule();
        }

        /*public void CargarProductos()
        {
            Vista.Content = new ProductsModule();
        }*/
    }
}
