using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lavanderia.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string CorreoElectronico { get; set; }
        public string Clave { get; set; }
        public string Numero { get; set; }
        public int? EmpleadoID { get; set; }
        public int RolID { get; set; }
        public bool Estado { get; set; }
        public Rol Rol { get; set; }
    }

}