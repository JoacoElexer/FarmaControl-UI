using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks; // A�n la necesitas para 'Task' en otros m�todos
using System.Diagnostics;
using System; // Agrega esto para EventArgs y DateTime

namespace FarmaControl_App.Views
{
    public partial class CashierModule : ContentView
    {
        // =======================================================================================
        // *** CONFIGURACI�N CR�TICA: URL DE TU API ***
        // (Mant�n esto como lo tengas configurado para tu entorno)
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

        // --- M�TODOS DE EVENTO PARA LOS BOTONES ---

        // Maneja el clic en el bot�n "A�adir al Carrito"
        // VUELVE A async void para que coincida con la expectativa del XAML
        private async void OnAddProductToCartClicked(object sender, EventArgs e)
        {
            string productQuery = ProductSearchEntry.Text;
            if (!string.IsNullOrWhiteSpace(productQuery))
            {
                await DisplayModuleAlert("Producto A�adido (Simulado)", $"Se intent� a�adir: '{productQuery}'.\n\n�Implementa la l�gica real del carrito aqu�!");
                ProductSearchEntry.Text = string.Empty;
                UpdateTotals(100.00m, 16.00m, 116.00m);
            }
            else
            {
                await DisplayModuleAlert("Error", "Por favor, introduce un c�digo o nombre de producto para a�adir.");
            }
        }

        // Maneja el clic en el bot�n "Vaciar Carrito"
        // VUELVE A async void
        private async void OnClearCartClicked(object sender, EventArgs e)
        {
            await DisplayModuleAlert("Carrito", "El carrito ha sido vaciado.");
            UpdateTotals(0, 0, 0);
            LblCartItems.Text = "[Lista de productos en el carrito]";
        }

        // Maneja el clic en el bot�n "Finalizar Venta"
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
                        await DisplayModuleAlert("Venta Registrada", "La venta ha sido registrada con �xito.");
                        Debug.WriteLine($"Respuesta exitosa de la API: {responseContent}");

                        // Si OnClearCartClicked es async void, no puedes usar await directamente aqu�.
                        // Sin embargo, como est�s en un async void, puedes llamarlo.
                        OnClearCartClicked(sender, e); // Llama al m�todo sin await
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        await DisplayModuleAlert("Error al Registrar Venta", $"No se pudo registrar la venta. C�digo de estado: {(int)response.StatusCode}. Detalles: {errorContent}");
                        Debug.WriteLine($"Error de la API (C�digo: {(int)response.StatusCode}): {errorContent}");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    await DisplayModuleAlert("Error de Conexi�n", $"No se pudo conectar al servidor. Mensaje: {httpEx.Message}.\nAseg�rate de que el backend est� corriendo y accesible en: {ApiBaseUrl}ventas.");
                    Debug.WriteLine($"HttpRequestException (error de red): {httpEx.Message}");
                }
                catch (Exception ex)
                {
                    await DisplayModuleAlert("Error Inesperado", $"Ocurri� un error inesperado: {ex.Message}");
                    Debug.WriteLine($"Excepci�n general: {ex.Message}");
                }
            }
            else
            {
                await DisplayModuleAlert("Error de Datos", "Los valores de subtotal, impuestos o total no son v�lidos. Aseg�rate de que los datos de la UI sean n�meros v�lidos.");
            }
        }

        // Este m�todo NO es un manejador de evento XAML, as� que S� puede ser Task.
        private void UpdateTotals(decimal subtotal, decimal impuestos, decimal total)
        {
            LblSubtotal.Text = $"${subtotal:F2}";
            LblImpuestos.Text = $"${impuestos:F2}";
            LblTotal.Text = $"${total:F2}";
        }

        // Este m�todo NO es un manejador de evento XAML, as� que S� puede ser Task.
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
                Debug.WriteLine($"FALLBACK DISPLAY ALERT (No se pudo mostrar en UI) - T�tulo: {title}, Mensaje: {message}");
            }
        }
    }
}