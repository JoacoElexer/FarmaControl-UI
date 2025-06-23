using FarmaControl_App.Views; // Asegúrate de importar tu carpeta de Vistas

namespace FarmaControl_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // ================================================================
            // Registro de Rutas para la Navegación
            // Cada ruta asocia un nombre (string) con un tipo de página (Type)
            // Cuando se usa Shell.Current.GoToAsync("nombreRuta"), el Shell sabe
            // qué página cargar.
            // ================================================================

            /*Routing.RegisterRoute("mainpage", typeof(MainPage)); // Ya debería estar
            Routing.RegisterRoute("adminmodule", typeof(AdminModule)); // Si AdminModule fuera una página completa y quisieras navegar directamente a ella

            // Registra las rutas para cada una de tus páginas de administración
            Routing.RegisterRoute("userspage", typeof(UsersPage)); // Para la página de Usuarios
            Routing.RegisterRoute("clientspage", typeof(ClientsPage)); // Para la página de Clientes
            Routing.RegisterRoute("providerspage", typeof(ProvidersPage)); // Para la página de Proveedores
            Routing.RegisterRoute("purchasespage", typeof(PurchasesPage)); // Para la página de Compras
            Routing.RegisterRoute("salespage", typeof(SalesPage)); // Para la página de Ventas
            Routing.RegisterRoute("productspage", typeof(ProductsPage)); // Para la página de Productos
            Routing.RegisterRoute("reportspage", typeof(ReportsPage)); // Para la página de Reportes*/
        }
    }
}