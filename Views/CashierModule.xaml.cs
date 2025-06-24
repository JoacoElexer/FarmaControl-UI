using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text.Json; // Importa para usar JsonSerializer
using System.Collections.Generic; // Importa para usar List<T>
using System.Threading.Tasks; // Importa para usar Task (para métodos async)
using System.Diagnostics; // Importa para usar Debug.WriteLine (para depuración en Visual Studio)

namespace FarmaControl_App.Views
{
    /*public partial class CashierModule : ContentView
    {
        // =======================================================================================
        // *** CONFIGURACIÓN CRÍTICA: URL DE TU API ***
        // Elige la URL correcta según dónde estés ejecutando tu aplicación MAUI:
        // =======================================================================================

        // 1. SI USAS UN EMULADOR DE ANDROID:
        //    '10.0.2.2' es un alias especial para 'localhost' de tu máquina de desarrollo.
        private const string ApiBaseUrl = "http://localhost:3000/api/";

        // 2. SI EJECUTAS LA APLICACIÓN MAUI EN WINDOWS (y tu backend está en el mismo PC):
        //    Descomenta la siguiente línea y comenta la de arriba si estás en Windows.
        // private const string ApiBaseUrl = "http://10.0.2.2:3000/api/";

        // 3. SI USAS UN DISPOSITIVO FÍSICO (Android/iOS) conectado a la misma red Wi-Fi que tu PC:
        //    Deberás encontrar la dirección IP local de tu PC (ej. ejecuta 'ipconfig' en CMD para Windows)
        //    Descomenta la siguiente línea y reemplaza 'TU_IP_LOCAL_DEL_PC' con la IP real.
        // private const string ApiBaseUrl = "http://TU_IP_LOCAL_DEL_PC:3000/api/";
        // (Ejemplo: "http://192.168.1.100:3000/api/")
        // =======================================================================================


        private readonly HttpClient _httpClient;

        // Clase que representa la estructura de los datos que TU API de ventas espera.
        // Las propiedades de esta clase DEBEN COINCIDIR EXACTAMENTE
        // con los nombres de las columnas en tu tabla 'compras' en la base de datos MySQL,
        // o con las claves que tu backend espera en el objeto 'req.body'.
        public class VentaPayload
        {
            // Basado en tu archivo ddl_farmacia.sql y el modelo 'compras'/'ventas':
            // Las columnas son: id, proveedor_id, usuario_id, fecha_compra, total.

            // ** MUY IMPORTANTE SOBRE 'proveedor_id': **
            // Tu tabla 'compras' tiene 'proveedor_id'. Si este endpoint es para 'ventas' (a clientes),
            // es una inconsistencia lógica. DEBES ENVIAR UN VALOR QUE EXISTA EN TU TABLA 'proveedores'.
            // Consulta con tus compañeros qué 'proveedor_id' se debe usar para las ventas a clientes.
            public int proveedor_id { get; set; } // Ejemplo: 1 (debe ser un ID válido de tu tabla 'proveedores')

            // Este es el ID del usuario/cajero que realiza la venta.
            // DEBES ENVIAR UN VALOR QUE EXISTA EN TU TABLA 'usuarios'.
            public int usuario_id { get; set; } // Ejemplo: 2 (un ID válido de tu tabla 'usuarios', ej. 'cajero01')

            // La columna en tu DB es 'fecha_compra'. Se mapea a DateTime en C#.
            // Usamos DateTime.UtcNow para la fecha y hora actual en formato UTC.
            public DateTime fecha_compra { get; set; }

            // La columna en tu DB es 'total'.
            // Esta será el valor final del carrito.
            public decimal total { get; set; }

            // NOTA: Tu tabla 'compras' NO tiene columnas separadas para 'subtotal' ni 'impuestos'.
            // Solo se envía el 'total'. Si estos campos son necesarios en la DB, tu esquema
            // y modelo de backend deben ser actualizados primero.
        }


        // --- CONSTRUCTOR ---
        public CashierModule()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30); // Tiempo de espera máximo para peticiones HTTP

            // Inicializa los Labels de totales en la UI a $0.00 al cargar el módulo.
            UpdateTotals(0, 0, 0);
        }

        // --- MÉTODOS DE EVENTO PARA LOS BOTONES ---

        // Maneja el clic en el botón "Añadir al Carrito"
        private async Task OnAddProductToCartClicked(object sender, EventArgs e)
        {
            string productQuery = ProductSearchEntry.Text; // Obtiene el texto del campo de búsqueda
            if (!string.IsNullOrWhiteSpace(productQuery))
            {
                // *** Lógica REAL para añadir productos al carrito (debes implementarla) ***
                // 1. Buscar el producto en tu base de datos o API de productos.
                // 2. Añadir el producto a una lista interna (ej. List<ProductoCarrito> o ObservableCollection<ProductoCarrito>).
                // 3. Recalcular el subtotal, impuestos y total del carrito.
                // 4. Actualizar los Labels en la UI (LblCartItems, LblSubtotal, LblImpuestos, LblTotal) con los nuevos valores.

                await DisplayModuleAlert("Producto Añadido (Simulado)", $"Se intentó añadir: '{productQuery}'.\n\n¡Implementa la lógica real del carrito aquí!");
                ProductSearchEntry.Text = string.Empty; // Limpia el campo de búsqueda

                // *** SIMULACIÓN: Actualiza los totales para poder probar la venta ***
                // Estos son valores de ejemplo. En tu aplicación real, serían calculados.
                UpdateTotals(100.00m, 16.00m, 116.00m);
            }
            else
            {
                await DisplayModuleAlert("Error", "Por favor, introduce un código o nombre de producto para añadir.");
            }
        }

        // Maneja el clic en el botón "Vaciar Carrito"
        private async Task OnClearCartClicked(object sender, EventArgs e)
        {
            // *** Lógica REAL para vaciar el carrito (debes implementarla) ***
            // 1. Limpiar la lista interna de productos del carrito.
            // 2. Reiniciar el subtotal, impuestos y total a cero.
            // 3. Actualizar los Labels en la UI.
            await DisplayModuleAlert("Carrito", "El carrito ha sido vaciado.");
            UpdateTotals(0, 0, 0); // Restablece los totales en la UI
            LblCartItems.Text = "[Lista de productos en el carrito]"; // Restablece el texto de placeholder
        }

        // Maneja el clic en el botón "Finalizar Venta"
        private async Task OnCheckoutClicked(object sender, EventArgs e)
        {
            await DisplayModuleAlert("Venta", "Intentando registrar la venta...");

            decimal subtotalUI = 0;
            decimal impuestosUI = 0;
            decimal totalUI = 0;

            // Intenta parsear los valores de los Labels de la UI.
            // Es crucial que el texto de los Labels sea un número válido.
            bool parseSuccess = decimal.TryParse(LblSubtotal.Text.Replace("$", ""), out subtotalUI) &&
                                decimal.TryParse(LblImpuestos.Text.Replace("$", ""), out impuestosUI) &&
                                decimal.TryParse(LblTotal.Text.Replace("$", ""), out totalUI);

            if (parseSuccess)
            {
                // **1. Construir el objeto VentaPayload con los datos del carrito (y los IDs requeridos)**
                var ventaParaEnviar = new VentaPayload
                {
                    // ¡RECUERDA AJUSTAR ESTOS IDs CON VALORES VÁLIDOS DE TU DB!
                    proveedor_id = 1,      // <-- ID de un proveedor existente en tu tabla 'proveedores'
                    usuario_id = 2,        // <-- ID de un usuario/cajero existente en tu tabla 'usuarios'
                    fecha_compra = DateTime.UtcNow, // Fecha y hora actual en UTC
                    total = totalUI        // Utiliza el total que se muestra en la UI
                };

                // **2. Serializar el objeto C# a una cadena JSON**
                var jsonOptions = new JsonSerializerOptions
                {
                    // No se necesita PropertyNamingPolicy aquí si los nombres de propiedades en VentaPayload
                    // ya coinciden con las claves esperadas por el backend (ej. snake_case como en la DB).
                    // Si tu backend espera camelCase (ej. 'proveedorId'), deberías usar:
                    // PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true // Formato JSON legible para depuración (eliminar en producción)
                };
                string jsonContent = JsonSerializer.Serialize(ventaParaEnviar, jsonOptions);

                Debug.WriteLine($"JSON a enviar: {jsonContent}"); // Imprime el JSON en la ventana de Salida de depuración

                // **3. Crear el contenido HTTP para la petición POST**
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                // **4. Realizar la petición POST a la API**
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsync($"{ApiBaseUrl}ventas", content);

                    if (response.IsSuccessStatusCode) // Verifica si el código de estado es 2xx (ej. 200 OK, 201 Created)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        await DisplayModuleAlert("Venta Registrada", "La venta ha sido registrada con éxito.");
                        Debug.WriteLine($"Respuesta exitosa de la API: {responseContent}");

                        // Opcional: Si el backend devuelve el ID de la venta, podrías usarlo aquí.
                        // var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                        // Debug.WriteLine($"ID de la venta: {apiResponse?.id}");

                        // Después de registrar la venta, limpia el carrito.
                        await OnClearCartClicked(sender, e);
                    }
                    else
                    {
                        // Si la API devuelve un código de error (ej. 400 Bad Request, 500 Internal Server Error)
                        string errorContent = await response.Content.ReadAsStringAsync();
                        await DisplayModuleAlert("Error al Registrar Venta", $"No se pudo registrar la venta. Código de estado: {(int)response.StatusCode}. Detalles: {errorContent}");
                        Debug.WriteLine($"Error de la API (Código: {(int)response.StatusCode}): {errorContent}");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    // Captura errores de red (ej. servidor no está corriendo, URL incorrecta, problemas de conexión)
                    await DisplayModuleAlert("Error de Conexión", $"No se pudo conectar al servidor. Mensaje: {httpEx.Message}.\nAsegúrate de que el backend esté corriendo y accesible en: {ApiBaseUrl}ventas.");
                    Debug.WriteLine($"HttpRequestException (error de red): {httpEx.Message}");
                }
                catch (Exception ex)
                {
                    // Captura cualquier otro tipo de error inesperado
                    await DisplayModuleAlert("Error Inesperado", $"Ocurrió un error inesperado: {ex.Message}");
                    Debug.WriteLine($"Excepción general: {ex.Message}");
                }
            }
            else
            {
                // Si los valores de los Labels no se pudieron convertir a números
                await DisplayModuleAlert("Error de Datos", "Los valores de subtotal, impuestos o total no son válidos. Asegúrate de que los datos de la UI sean números válidos.");
            }
        }

        // Método auxiliar para actualizar el texto de los Labels de totales en la UI
        private void UpdateTotals(decimal subtotal, decimal impuestos, decimal total)
        {
            // Formatea los valores a 2 decimales y añade el símbolo '$'.
            LblSubtotal.Text = $"${subtotal:F2}";
            LblImpuestos.Text = $"${impuestos:F2}";
            LblTotal.Text = $"${total:F2}";
        }

        // Método auxiliar para mostrar alertas de forma segura y consistente.
        private async Task DisplayModuleAlert(string title, string message)
        {
            // Intenta mostrar la alerta usando la Page de la Window a la que pertenece este ContentView
            if (this.Window?.Page != null)
            {
                await this.Window.Page.DisplayAlert(title, message, "OK");
            }
            // Si no está adjunto a una Window (poco común para un módulo visible), intenta usar Shell.Current
            else if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlert(title, message, "OK");
            }
            else
            {
                // Como último recurso, o para depuración si no se puede mostrar la UI
                Debug.WriteLine($"FALLBACK DISPLAY ALERT (No se pudo mostrar en UI) - Título: {title}, Mensaje: {message}");
            }
        }
    }*/
}