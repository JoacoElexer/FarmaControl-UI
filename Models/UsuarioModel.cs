using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaControl_App.Models
{
    class UsuarioModel
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string contrasenia { get; set; }
        public string nombre_Completo { get; set; }
        public string email { get; set; }
        public string rol { get; set; }
    }
}
