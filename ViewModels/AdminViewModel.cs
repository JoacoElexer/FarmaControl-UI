// FarmaControl_UI.ViewModels.AdminViewModel.cs

// Usar estos namespaces si tienes instalado CommunityToolkit.Mvvm
using CommunityToolkit.Mvvm.ComponentModel; // Para ObservableObject (opcional, pero buena práctica)
using CommunityToolkit.Mvvm.Input; // Para AsyncRelayCommand e IAsyncRelayCommand

using Microsoft.Maui.Controls; // Para Shell
using System.Threading.Tasks; // Necesario para métodos async

namespace FarmaControl_UI.ViewModels;

public partial class AdminViewModel : ObservableObject
{
    // Define tus comandos usando IAsyncRelayCommand
    public IAsyncRelayCommand IrAUsuariosCommand { get; }
    public IAsyncRelayCommand IrAClientesCommand { get; } 
    public IAsyncRelayCommand IrAProveedoresCommand { get; }
    public IAsyncRelayCommand IrAComprasCommand { get; }
    public IAsyncRelayCommand IrAVentasCommand { get; }
    public IAsyncRelayCommand IrAProductosCommand { get; }
    public IAsyncRelayCommand IrAReportesCommand { get; }

    public AdminViewModel()
    {
        // Inicializa los comandos, apuntando a los métodos asíncronos
        IrAUsuariosCommand = new AsyncRelayCommand(IrAUsuarios);
        IrAClientesCommand = new AsyncRelayCommand(IrAClientes); // Asegúrate de inicializar este también
        IrAProveedoresCommand = new AsyncRelayCommand(IrAProveedores);
        IrAComprasCommand = new AsyncRelayCommand(IrACompras);
        IrAVentasCommand = new AsyncRelayCommand(IrAVentas);
        IrAProductosCommand = new AsyncRelayCommand(IrAProductos);
        IrAReportesCommand = new AsyncRelayCommand(IrAReportes);
    }

    // Métodos que ejecutan la navegación
    private async Task IrAUsuarios()
    {
        await Shell.Current.GoToAsync("//UsuariosPage");
    }

    private async Task IrAClientes() // Agrega este método si no lo tienes
    {
        await Shell.Current.GoToAsync("//ClientesPage");
    }

    private async Task IrAProveedores()
    {
        await Shell.Current.GoToAsync("//ProveedoresPage");
    }

    private async Task IrACompras()
    {
        await Shell.Current.GoToAsync("//ComprasPage");
    }

    private async Task IrAVentas()
    {
        await Shell.Current.GoToAsync("//VentasPage");
    }

    private async Task IrAProductos()
    {
        await Shell.Current.GoToAsync("//ProductosPage");
    }

    private async Task IrAReportes()
    {
        await Shell.Current.GoToAsync("//ReportesPage");
    }
}