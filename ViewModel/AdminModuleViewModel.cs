// Importaciones necesarias para el ViewModel
using CommunityToolkit.Mvvm.Input; // Requiere el paquete NuGet CommunityToolkit.Mvvm
using System.Windows.Input; // Para ICommand
using Microsoft.Maui.Controls; // Para Shell.Current

namespace FarmaControl_App.ViewModels
{
    // El ViewModel para la vista AdminModule
    public class AdminModuleViewModel
    {
        // Propiedad de comando para navegar a la página de Usuarios
        /*public ICommand IrAUsuariosCommand { get; }
        // Propiedad de comando para navegar a la página de Clientes
        public ICommand IrAClientesCommand { get; }
        // Propiedad de comando para navegar a la página de Proveedores
        public ICommand IrAProveedoresCommand { get; }
        // Propiedad de comando para navegar a la página de Compras
        public ICommand IrAComprasCommand { get; }
        // Propiedad de comando para navegar a la página de Ventas
        public ICommand IrAVentasCommand { get; }
        // Propiedad de comando para navegar a la página de Productos
        public ICommand IrAProductosCommand { get; }
        // Propiedad de comando para navegar a la página de Reportes
        public ICommand IrAReportesCommand { get; }

        public AdminModuleViewModel()
        {
            // Inicialización de los comandos, cada uno llamará a un método privado de navegación
            IrAUsuariosCommand = new RelayCommand(async () => await IrAUsuarios());
            IrAClientesCommand = new RelayCommand(async () => await IrAClientes());
            IrAProveedoresCommand = new RelayCommand(async () => await IrAProveedores());
            IrAComprasCommand = new RelayCommand(async () => await IrACompras());
            IrAVentasCommand = new RelayCommand(async () => await IrAVentas());
            IrAProductosCommand = new RelayCommand(async () => await IrAProductos());
            IrAReportesCommand = new RelayCommand(async () => await IrAReportes());
        }

        // Métodos de navegación asíncronos que utilizan Shell.Current.GoToAsync()
        // Las rutas deben estar registradas en AppShell.xaml.cs
        private async Task IrAUsuarios()
        {
            // Navega a la ruta "userspage"
            await Shell.Current.GoToAsync("userspage");
        }

        private async Task IrAClientes()
        {
            // Navega a la ruta "clientspage"
            await Shell.Current.GoToAsync("clientspage");
        }

        private async Task IrAProveedores()
        {
            // Navega a la ruta "providerspage"
            await Shell.Current.GoToAsync("providerspage");
        }

        private async Task IrACompras()
        {
            // Navega a la ruta "purchasespage"
            await Shell.Current.GoToAsync("purchasespage");
        }

        private async Task IrAVentas()
        {
            // Navega a la ruta "salespage"
            await Shell.Current.GoToAsync("salespage");
        }

        private async Task IrAProductos()
        {
            // Navega a la ruta "productspage"
            await Shell.Current.GoToAsync("productspage");
        }

        private async Task IrAReportes()
        {
            // Navega a la ruta "reportspage"
            await Shell.Current.GoToAsync("reportspage");
        }*/
    }
}
