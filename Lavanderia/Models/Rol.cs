using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lavanderia.Models
{
    public class Rol
    {
        public int RolID { get; set; }
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}