using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using System.Windows.Input;
using FarmaControl_UI.Models;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.ViewModels
{
    public class UserFormViewModel : INotifyPropertyChanged
    {
        private int id;
        private string user;
        private string email;
        private string rol;
        private string mensaje;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string User
        {
            get => user;
            set { user = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string Rol
        {
            get => rol;
            set { rol = value; OnPropertyChanged(); }
        }

        public string Mensaje
        {
            get => mensaje;
            set { mensaje = value; OnPropertyChanged(); }
        }

        public bool EsEdicion { get; set; } = false;

        public string TextoBoton => EsEdicion ? "Actualizar" : "Registrar";

        public ICommand GuardarCommand { get; }

        public UserFormViewModel()
        {
            GuardarCommand = new Command(async () => await GuardarUsuario());
        }

        public void Inicializar(Usuario usuario)
        {
            if (usuario != null)
            {
                Id = usuario.Id;
                User = usuario.User;
                Email = usuario.Email;
                Rol = usuario.Rol;
                EsEdicion = true;
            }
            else
            {
                EsEdicion = false;
            }

            OnPropertyChanged(nameof(TextoBoton));
        }

        private async Task GuardarUsuario()
        {
            try
            {
                var http = new HttpClient();
                Usuario nuevoUsuario = new()
                {
                    Id = Id,
                    User = User,
                    Email = Email,
                    Rol = Rol
                };

                HttpResponseMessage response;

                if (EsEdicion)
                {
                    response = await http.PutAsJsonAsync($"http://localhost:3000/api/usuarios/{Id}", nuevoUsuario);
                }
                else
                {
                    response = await http.PostAsJsonAsync("http://localhost:3000/api/usuarios", nuevoUsuario);
                }

                if (response.IsSuccessStatusCode)
                {
                    Mensaje = EsEdicion ? "Usuario actualizado correctamente." : "Usuario registrado correctamente.";
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    Mensaje = "Error al guardar el usuario.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
