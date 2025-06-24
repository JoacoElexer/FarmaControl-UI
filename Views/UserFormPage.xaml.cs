using FarmaControl_UI.Models;
using FarmaControl_UI.ViewModels;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.Views
{
    [QueryProperty(nameof(Usuario), "Usuario")]
    public partial class UserFormPage : ContentPage
    {
        public Usuario Usuario { get; set; }

        public UserFormPage()
        {
            InitializeComponent();
            BindingContext = new UserFormViewModel(); // Instanciamos el ViewModel
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Si estamos editando, pasamos el usuario al ViewModel
            if (BindingContext is UserFormViewModel vm)
            {
                vm.Inicializar(Usuario);
            }
        }
    }
}
