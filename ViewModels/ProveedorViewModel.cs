using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using FarmaControl_UI.Models;
using FarmaControl_UI.Services;

namespace FarmaControl_UI.ViewModels;

public class ProveedorViewModel : INotifyPropertyChanged
{
    private string filtro;
    public string Filtro
    {
        get => filtro;
        set
        {
            filtro = value;
            OnPropertyChanged();
            FiltrarProveedores();
        }
    }

    public ObservableCollection<Proveedor> Proveedores { get; set; } = new();
    private List<Proveedor> todosLosProveedores = new();


    public ProveedorViewModel()
    {
        _ = CargarProveedores();
    }

    private async Task CargarProveedores()
    {
        try
        {
            var http = new HttpClient();
            var response = await http.GetFromJsonAsync<List<Proveedor>>("http://localhost:3000/api/proveedores");

            if (response != null)
            {
                todosLosProveedores = response;
                FiltrarProveedores();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar proveedores: {ex.Message}");
        }
    }

    private void FiltrarProveedores()
    {
        var filtrados = string.IsNullOrWhiteSpace(Filtro)
            ? todosLosProveedores
            : todosLosProveedores.Where(p =>
                p.Nombre_Empresa.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ||
                p.Email_Contacto.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ||
                p.Direccion.Contains(Filtro, StringComparison.OrdinalIgnoreCase)).ToList();

        Proveedores.Clear();
        foreach (var p in filtrados)
            Proveedores.Add(p);
    }

    public bool PuedeModificar => UserSession.UsuarioActual?.Rol.ToLower() == "administrador";


    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
