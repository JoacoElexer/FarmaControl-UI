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
                string apiUrl = $"http://localhost:3000/api/usuarios?email={correo}&contrasenia={password}";

                HttpResponseMessage respuesta = await cliente.GetAsync(apiUrl);

                if (respuesta.IsSuccessStatusCode)
                {
                    string json = await respuesta.Content.ReadAsStringAsync();
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
