using Microsoft.Maui.Controls; // Asegúrate de tener esta importación

namespace FarmaControl_App.Views
{
    public partial class CashierModule : ContentView // Debe ser ContentView aquí
    {
        public CashierModule()
        {
            InitializeComponent();
            // Si creas un ViewModel para este módulo en el futuro, lo establecerías aquí:
            // this.BindingContext = new ViewModels.CashierModuleViewModel();
        }

        // =========================================================================
        // Métodos de Evento para los botones Clicked
        // Asegúrate de que estos métodos existan y tengan esta firma exacta.
        // =========================================================================

        private async void OnAddProductToCartClicked(object sender, EventArgs e) // Marcar como async
        {
            // Accede al Entry por su x:Name para obtener el texto
            string productQuery = ProductSearchEntry.Text;
            if (!string.IsNullOrWhiteSpace(productQuery))
            {
                // Lógica simulada para añadir el producto al carrito
                // En una app real, esto interactuaría con un ViewModel o servicio de datos
                await Shell.Current.DisplayAlert("Producto Añadido", $"Se intentó añadir: {productQuery}", "OK"); // Usar Shell.Current.DisplayAlert
                ProductSearchEntry.Text = string.Empty; // Limpiar el campo de entrada
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, introduce un producto para añadir.", "OK"); // Usar Shell.Current.DisplayAlert
            }
        }

        private async void OnClearCartClicked(object sender, EventArgs e) // Marcar como async
        {
            // Lógica simulada para vaciar el carrito
            await Shell.Current.DisplayAlert("Carrito", "El carrito ha sido vaciado (simulado).", "OK"); // Usar Shell.Current.DisplayAlert
        }

        private async void OnCheckoutClicked(object sender, EventArgs e) // Marcar como async
        {
            // Lógica simulada para finalizar la venta y procesar el pago
            await Shell.Current.DisplayAlert("Venta", "Procesando el cobro (simulado)...", "OK"); // Usar Shell.Current.DisplayAlert
            // Aquí podrías navegar a una página de confirmación de pago, etc.
        }
    }
}