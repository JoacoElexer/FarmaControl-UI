using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaControl_UI.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        public string RolYEmail => $"{Rol} - {Email}";

    }

}
