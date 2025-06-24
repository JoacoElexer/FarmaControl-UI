using FarmaControl_App.Views;

namespace FarmaControl_App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
<<<<<<< HEAD
            //CargarLogin();
            CargarCashier();
=======
            //CargarUsuarios();
            CargarLogin();
>>>>>>> 258b078f18a0833b3d3303e3f094d102987428cf
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
<<<<<<< HEAD
=======

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
>>>>>>> 258b078f18a0833b3d3303e3f094d102987428cf
    }
}
