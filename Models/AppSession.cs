using FarmaControl_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaControl_UI.Models
{
    public static class AppSession
    {
        public static UsuarioModel? UsuarioAutenticado { get; set; }

    }
}
