using FarmaControl_UI.Models;
using FarmaControl_UI.ViewModels;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.Views
{
    [QueryProperty(nameof(Proveedor), "Proveedor")]
    public partial class ProveedorFormPage : ContentPage
    {
        public Proveedor Proveedor { get; set; }

        public ProveedorFormPage()
        {
            InitializeComponent();
            BindingContext = new ProveedorFormViewModel(); // Asignar el ViewModel
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ProveedorFormViewModel vm)
            {
                vm.Inicializar(Proveedor); // Carga datos si estamos editando
            }
        }
    }
}
