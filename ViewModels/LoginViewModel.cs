﻿using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Net.Http.Json;
using FarmaControl_UI.Models;
using Microsoft.Maui.Controls;
using FarmaControl_UI.Views;
using FarmaControl_UI.Services; // 👈 Asegúrate de agregar esto

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
            Mensaje = "Iniciando sesión...";
            var http = new HttpClient();
            var response = await http.PostAsJsonAsync("http://localhost:3000/api/auth/login", new
            {
                email = this.Email,
                contrasenia = this.Contrasenia
            });

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<Usuario>();

                //Guardamos el usuario autenticado
                UserSession.UsuarioActual = json;

                Mensaje = $"Bienvenido, {json.User} ({json.Rol})";

                string route = string.Empty;
                switch (json.Rol.ToLower())
                {
                    case "administrador":
                        route = $"//{nameof(AdminModule)}";
                        break;
                    case "cajero":
                        route = $"//{nameof(CashierModule)}";
                        break;
                    case "farmaceutico":
                        route = $"//{nameof(FarmaceuticModule)}";
                        break;
                    default:
                        Mensaje = "Rol no reconocido.";
                        return;
                }

                if (!string.IsNullOrEmpty(route))
                {
                    await Shell.Current.GoToAsync(route, true);
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                }
            }
            else
            {
                Mensaje = "Credenciales incorrectas.";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("localhost") || ex.Message.Contains("connection refused"))
            {
                Mensaje = "Error de conexión: Asegúrate de que el servidor esté en ejecución.";
            }
            else
            {
                Mensaje = "Error de conexión: " + ex.Message;
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
