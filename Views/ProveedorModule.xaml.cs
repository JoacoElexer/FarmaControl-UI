using FarmaControl_UI.Models;
using FarmaControl_UI.Views;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.Views
{
    public partial class ProveedorModule : ContentPage
    {
        public ProveedorModule()
        {
            InitializeComponent();
        }

        // Evento cuando se selecciona un proveedor de la lista para editar
        private async void OnProveedorSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Proveedor proveedorSeleccionado)
            {
                await Shell.Current.GoToAsync(nameof(ProveedorFormPage), new Dictionary<string, object>
                {
                    { "Proveedor", proveedorSeleccionado }
                });

                ((CollectionView)sender).SelectedItem = null; // Deselecciona visualmente
            }
        }

        // Evento al presionar el botón para agregar nuevo proveedor
        private async void OnAgregarProveedorClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProveedorFormPage)); // No se pasa proveedor: modo creación
        }
    }
}
