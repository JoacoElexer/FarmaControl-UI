using System.Windows.Input;

namespace FarmaControl_UI.ViewModels;

public class AdminViewModel
{
    public ICommand IrAUsuariosCommand { get; }
    public ICommand IrAProductosCommand { get; }
    public ICommand IrAProveedoresCommand { get; }
    public ICommand IrAComprasCommand { get; }
    public ICommand IrAVentasCommand { get; }
    public ICommand IrAReportesCommand { get; }

    public AdminViewModel()
    {
        IrAUsuariosCommand = new Command(async () => await Shell.Current.GoToAsync("//UsuariosPage"));
        IrAProductosCommand = new Command(async () => await Shell.Current.GoToAsync("//ProductosPage"));
        IrAProveedoresCommand = new Command(async () => await Shell.Current.GoToAsync("//ProveedoresPage"));
        IrAComprasCommand = new Command(async () => await Shell.Current.GoToAsync("//ComprasPage"));
        IrAVentasCommand = new Command(async () => await Shell.Current.GoToAsync("//VentasPage"));
        IrAReportesCommand = new Command(async () => await Shell.Current.GoToAsync("//ReportesPage"));
    }
}
