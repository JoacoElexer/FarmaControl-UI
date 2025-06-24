using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json; // Necesario para ReadFromJsonAsync, PostAsJsonAsync, etc.
using System.Threading.Tasks;
using FarmaControl_App.Models; // Para poder usar tu modelo 'User'

namespace FarmaControl_App.Services // ¡Asegúrate de que el namespace coincida con la ruta!
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public UserService()
        {
            _httpClient = new HttpClient();

            // Define la URL base de tu API según la plataforma.
            // Si la API está en http://localhost:3000/api/usuarios:
            // Para emuladores Android, 'localhost' de tu PC es '10.0.2.2'.
            // Para Windows, iOS Simulator o Mac Catalyst, es 'localhost'.
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                _baseApiUrl = "http://10.0.2.2:3000/api/usuarios";
            }
            else
            {
                _baseApiUrl = "http://localhost:3000/api/usuarios";
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                // Realiza la solicitud GET a la API
                var response = await _httpClient.GetAsync(_baseApiUrl);
                response.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es 2xx

                // Deserializa la respuesta JSON a una lista de objetos User
                return await response.Content.ReadFromJsonAsync<List<User>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener usuarios desde la API: {ex.Message}");
                return new List<User>(); // Devuelve una lista vacía en caso de error
            }
        }

        // Aquí puedes añadir métodos para otras operaciones CRUD (Create, Update, Delete)
        // si tu API los soporta:

        public async Task SaveUserAsync(User user)
        {
            HttpResponseMessage response;
            if (user.Id != 0) // Asumimos que un Id != 0 significa una actualización
            {
                response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}/{user.Id}", user);
            }
            else // Id == 0 significa una nueva creación
            {
                response = await _httpClient.PostAsJsonAsync(_baseApiUrl, user);
            }
            response.EnsureSuccessStatusCode(); // Asegúrate de que la operación fue exitosa
        }

        public async Task DeleteUserAsync(User user)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}/{user.Id}");
            response.EnsureSuccessStatusCode(); // Asegúrate de que la operación fue exitosa
        }
    }
}