using FarmaControl_UI.Models;
using FarmaControl_UI.Views;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.Views
{
    public partial class UserModule : ContentPage
    {
        public UserModule()
        {
            InitializeComponent();
        }

        // Evento cuando se selecciona un usuario de la lista para editar
        private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Usuario usuarioSeleccionado)
            {
                await Shell.Current.GoToAsync(nameof(UserFormPage), new Dictionary<string, object>
                {
                    { "Usuario", usuarioSeleccionado }
                });

                // Deseleccionar visualmente el ítem
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        // Evento cuando se presiona el botón para agregar un nuevo usuario
        private async void OnAgregarUsuarioClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(UserFormPage)); // Sin pasar usuario = nuevo
        }
    }
}
