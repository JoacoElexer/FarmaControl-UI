using System.Text.Json.Serialization;

namespace FarmaControl_UI.Models
{
    public class Usuario
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuario")]
        public string User { get; set; }

        [JsonPropertyName("contrasenia")]
        public string Contrasenia { get; set; }

        [JsonPropertyName("nombre_completo")]
        public string NombreCompleto { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("rol")]
        public string Rol { get; set; }

        // Propiedad útil para mostrar resumen en UI
        public string RolYEmail => $"{Rol} - {Email}";
    }
}
