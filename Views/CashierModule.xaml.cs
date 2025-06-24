using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks; // Aún la necesitas para 'Task' en otros métodos
using System.Diagnostics;
using System; // Agrega esto para EventArgs y DateTime

namespace FarmaControl_App.Views
{
    public partial class CashierModule : ContentView
    {
        // =======================================================================================
        // *** CONFIGURACIÓN CRÍTICA: URL DE TU API ***
        // (Mantén esto como lo tengas configurado para tu entorno)
        // =======================================================================================
        private const string ApiBaseUrl = "http://localhost:3000/api/"; // O tu IP/localhost

        private readonly HttpClient _httpClient;

        public class VentaPayload
        {
            public int proveedor_id { get; set; }
            public int usuario_id { get; set; }
            public DateTime fecha_compra { get; set; }
            public decimal total { get; set; }
        }

        // --- CONSTRUCTOR ---
        public CashierModule()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            UpdateTotals(0, 0, 0);
        }

        // --- MÉTODOS DE EVENTO PARA LOS BOTONES ---

        // Maneja el clic en el botón "Añadir al Carrito"
        // VUELVE A async void para que coincida con la expectativa del XAML
        private async void OnAddProductToCartClicked(object sender, EventArgs e)
        {
            string productQuery = ProductSearchEntry.Text;
            if (!string.IsNullOrWhiteSpace(productQuery))
            {
                await DisplayModuleAlert("Producto Añadido (Simulado)", $"Se intentó añadir: '{productQuery}'.\n\n¡Implementa la lógica real del carrito aquí!");
                ProductSearchEntry.Text = string.Empty;
                UpdateTotals(100.00m, 16.00m, 116.00m);
            }
            else
            {
                await DisplayModuleAlert("Error", "Por favor, introduce un código o nombre de producto para añadir.");
            }
        }

        // Maneja el clic en el botón "Vaciar Carrito"
        // VUELVE A async void
        private async void OnClearCartClicked(object sender, EventArgs e)
        {
            await DisplayModuleAlert("Carrito", "El carrito ha sido vaciado.");
            UpdateTotals(0, 0, 0);
            LblCartItems.Text = "[Lista de productos en el carrito]";
        }

        // Maneja el clic en el botón "Finalizar Venta"
        // VUELVE A async void
        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            await DisplayModuleAlert("Venta", "Intentando registrar la venta...");

            decimal subtotalUI = 0;
            decimal impuestosUI = 0;
            decimal totalUI = 0;

            bool parseSuccess = decimal.TryParse(LblSubtotal.Text.Replace("$", ""), out subtotalUI) &&
                                decimal.TryParse(LblImpuestos.Text.Replace("$", ""), out impuestosUI) &&
                                decimal.TryParse(LblTotal.Text.Replace("$", ""), out totalUI);

            if (parseSuccess)
            {
                var ventaParaEnviar = new VentaPayload
                {
                    proveedor_id = 1,
                    usuario_id = 2,
                    fecha_compra = DateTime.UtcNow,
                    total = totalUI
                };

                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonContent = JsonSerializer.Serialize(ventaParaEnviar, jsonOptions);

                Debug.WriteLine($"JSON a enviar: {jsonContent}");

                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsync($"{ApiBaseUrl}ventas", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        await DisplayModuleAlert("Venta Registrada", "La venta ha sido registrada con éxito.");
                        Debug.WriteLine($"Respuesta exitosa de la API: {responseContent}");

                        // Si OnClearCartClicked es async void, no puedes usar await directamente aquí.
                        // Sin embargo, como estás en un async void, puedes llamarlo.
                        OnClearCartClicked(sender, e); // Llama al método sin await
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        await DisplayModuleAlert("Error al Registrar Venta", $"No se pudo registrar la venta. Código de estado: {(int)response.StatusCode}. Detalles: {errorContent}");
                        Debug.WriteLine($"Error de la API (Código: {(int)response.StatusCode}): {errorContent}");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    await DisplayModuleAlert("Error de Conexión", $"No se pudo conectar al servidor. Mensaje: {httpEx.Message}.\nAsegúrate de que el backend esté corriendo y accesible en: {ApiBaseUrl}ventas.");
                    Debug.WriteLine($"HttpRequestException (error de red): {httpEx.Message}");
                }
                catch (Exception ex)
                {
                    await DisplayModuleAlert("Error Inesperado", $"Ocurrió un error inesperado: {ex.Message}");
                    Debug.WriteLine($"Excepción general: {ex.Message}");
                }
            }
            else
            {
                await DisplayModuleAlert("Error de Datos", "Los valores de subtotal, impuestos o total no son válidos. Asegúrate de que los datos de la UI sean números válidos.");
            }
        }

        // Este método NO es un manejador de evento XAML, así que SÍ puede ser Task.
        private void UpdateTotals(decimal subtotal, decimal impuestos, decimal total)
        {
            LblSubtotal.Text = $"${subtotal:F2}";
            LblImpuestos.Text = $"${impuestos:F2}";
            LblTotal.Text = $"${total:F2}";
        }

        // Este método NO es un manejador de evento XAML, así que SÍ puede ser Task.
        private async Task DisplayModuleAlert(string title, string message)
        {
            if (this.Window?.Page != null)
            {
                await this.Window.Page.DisplayAlert(title, message, "OK");
            }
            else if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlert(title, message, "OK");
            }
            else
            {
                Debug.WriteLine($"FALLBACK DISPLAY ALERT (No se pudo mostrar en UI) - Título: {title}, Mensaje: {message}");
            }
        }
    }
}