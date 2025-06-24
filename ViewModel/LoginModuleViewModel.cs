using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using FarmaControl_App.Models;

namespace FarmaControl_App.ViewModel
{
    class LoginModuleViewModel
    {
        private readonly HttpClient cliente = new HttpClient();

        public async Task<bool> VerificarLogin(string correo, string password)
        {
            try
            {
                string url = "http://localhost:3000/api/usuarios";

                HttpResponseMessage respuesta = await cliente.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    string json = await respuesta.Content.ReadAsStringAsync();

                    var listaUsuarios = JsonSerializer.Deserialize<List<UsuarioModel>>(json);

                    if (listaUsuarios == null) return false;

                    var usuarioEncontrado = listaUsuarios.FirstOrDefault(u =>
                        u.email == correo && u.contrasenia == password);

                    return usuarioEncontrado != null;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar login: {ex.Message}");
                return false;
            }
        }
    }
}
