using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FarmaControl_App.Models
{
    public class User
    {
        [JsonPropertyName("id")] // Mapea "id" del JSON a la propiedad Id en C#
        public int Id { get; set; }

        [JsonPropertyName("nombre_completo")] // Mapea "nombre_completo" del JSON a Name
        public string Name { get; set; }

        [JsonPropertyName("usuario")] // Mapea "usuario" del JSON a Username
        public string Username { get; set; }

        [JsonPropertyName("rol")] // Mapea "rol" del JSON a Role
        public string Role { get; set; }

        [JsonPropertyName("email")] // Mapea "email" del JSON a Email
        public string Email { get; set; }

        // La contraseña no es necesaria para mostrar en la UI, pero si la quieres en el modelo
        // para otras operaciones (ej. login), también deberías mapearla:
        [JsonPropertyName("contrasenia")]
        public string Contrasenia { get; set; } // O Password, lo que prefieras
    }
}
