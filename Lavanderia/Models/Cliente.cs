using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lavanderia.Models
{
    public class Cliente
    {
        
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Numero { get; set; }
        public bool Estado { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";

    }

}