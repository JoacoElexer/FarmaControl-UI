using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaControl_UI.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre_Empresa { get; set; } = string.Empty;
        public string Email_Contacto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty; 
        public string Direccion { get; set; } = string.Empty;

        public string ContactoResumido => $"Tel: {Telefono} | {Email_Contacto}";
    }

}
