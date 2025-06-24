using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaControl_App.Models
{
    class UsuarioModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre_Completo { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}
