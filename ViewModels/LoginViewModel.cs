using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Net.Http.Json;
using FarmaControl_UI.Models;

namespace FarmaControl_UI.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string email;
    private string contrasenia;
    private string mensaje;

    public string Email
    {
        get => email;
        set { email = value; OnPropertyChanged(); }
    }

    public string Contrasenia
    {
        get => contrasenia;
        set { contrasenia = value; OnPropertyChanged(); }
    }

    public string Mensaje
    {
        get => mensaje;
        set { mensaje = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(async () => await Login());
    }

    private async Task Login()
    {
        try
        {
            var http = new HttpClient();
            var response = await http.PostAsJsonAsync("http://localhost:3000/api/auth/login", new
            {
                email = this.Email,
                contrasenia = this.Contrasenia
            });

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<Usuario>();
                Mensaje = $"Bienvenido, {json.User} ({json.Rol})";

                // Aquí podrías navegar a otra página según el rol
                // ej: Shell.Current.GoToAsync("//AdminModule");
            }
            else
            {
                Mensaje = "Credenciales incorrectas.";
            }
        }
        catch (Exception ex)
        {
            Mensaje = "Error de conexión: " + ex.Message;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
