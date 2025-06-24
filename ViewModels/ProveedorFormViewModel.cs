using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using System.Windows.Input;
using FarmaControl_UI.Models;
using Microsoft.Maui.Controls;

namespace FarmaControl_UI.ViewModels
{
    public class ProveedorFormViewModel : INotifyPropertyChanged
    {
        private int id;
        private string nombreEmpresa;
        private string emailContacto;
        private string telefono;
        private string direccion;
        private string mensaje;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string Nombre_Empresa
        {
            get => nombreEmpresa;
            set { nombreEmpresa = value; OnPropertyChanged(); }
        }

        public string Email_Contacto
        {
            get => emailContacto;
            set { emailContacto = value; OnPropertyChanged(); }
        }

        public string Telefono
        {
            get => telefono;
            set { telefono = value; OnPropertyChanged(); }
        }

        public string Direccion
        {
            get => direccion;
            set { direccion = value; OnPropertyChanged(); }
        }

        public string Mensaje
        {
            get => mensaje;
            set { mensaje = value; OnPropertyChanged(); }
        }

        public bool EsEdicion { get; set; } = false;

        public string TextoBoton => EsEdicion ? "Actualizar" : "Registrar";
        public string TituloFormulario => EsEdicion ? "EDITAR PROVEEDOR" : "AGREGAR PROVEEDOR";

        public ICommand GuardarCommand { get; }

        public ProveedorFormViewModel()
        {
            GuardarCommand = new Command(async () => await GuardarProveedor());
        }

        public void Inicializar(Proveedor proveedor)
        {
            if (proveedor != null)
            {
                Id = proveedor.Id;
                Nombre_Empresa = proveedor.Nombre_Empresa;
                Email_Contacto = proveedor.Email_Contacto;
                Telefono = proveedor.Telefono;
                Direccion = proveedor.Direccion;
                EsEdicion = true;
            }
            else
            {
                EsEdicion = false;
            }
            OnPropertyChanged(nameof(TextoBoton));
            OnPropertyChanged(nameof(TituloFormulario));
        }

        private async Task GuardarProveedor()
        {
            try
            {
                var http = new HttpClient();
                Proveedor nuevo = new()
                {
                    Id = Id,
                    Nombre_Empresa = Nombre_Empresa,
                    Email_Contacto = Email_Contacto,
                    Telefono = Telefono,
                    Direccion = Direccion
                };

                HttpResponseMessage response;

                if (EsEdicion)
                {
                    response = await http.PutAsJsonAsync($"http://localhost:3000/api/proveedores/{Id}", nuevo);
                }
                else
                {
                    response = await http.PostAsJsonAsync("http://localhost:3000/api/proveedores", nuevo);
                }

                if (response.IsSuccessStatusCode)
                {
                    Mensaje = EsEdicion ? "Proveedor actualizado." : "Proveedor registrado.";
                }
                else
                {
                    Mensaje = "Error al guardar el proveedor.";
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
