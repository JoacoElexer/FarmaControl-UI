using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using FarmaControl_UI.Models;
using FarmaControl_UI.Services;

namespace FarmaControl_UI.ViewModels;

public class UserViewModel : INotifyPropertyChanged
{
    private string filtro;
    public string Filtro
    {
        get => filtro;
        set
        {
            filtro = value;
            OnPropertyChanged();
            FiltrarUsuarios();
        }
    }

    public ObservableCollection<Usuario> Usuarios { get; set; } = new();
    private List<Usuario> todosLosUsuarios = new();

    private string mensaje;
    public string Mensaje
    {
        get => mensaje;
        set { mensaje = value; OnPropertyChanged(); }
    }

    public UserViewModel()
    {
        _ = CargarUsuarios();
    }

    private async Task CargarUsuarios()
    {
        try
        {
            var http = new HttpClient();
            var response = await http.GetFromJsonAsync<List<Usuario>>("http://localhost:3000/api/usuarios");
            if (response != null)
            {
                todosLosUsuarios = response;
                FiltrarUsuarios();
                Mensaje = ""; // Limpia el mensaje si carga bien
            }
            else
            {
                Mensaje = "No se pudieron obtener los usuarios.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
            Mensaje = "Ocurrió un error al cargar los usuarios.";
        }
    }

    private void FiltrarUsuarios()
    {
        var filtrados = string.IsNullOrWhiteSpace(Filtro)
            ? todosLosUsuarios
            : todosLosUsuarios.Where(u =>
                (u.User?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (u.Email?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (u.Rol?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (u.NombreCompleto?.Contains(Filtro, StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToList();

        Usuarios.Clear();
        foreach (var u in filtrados)
            Usuarios.Add(u);
    }

    public bool PuedeModificar =>
    UserSession.UsuarioActual?.Rol.ToLower() is "administrador" or "farmaceutico";


    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
