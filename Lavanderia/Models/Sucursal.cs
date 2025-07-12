using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lavanderia.Models
{
    public class Sucursal
    {
        public int SucursalID { get; set; }
        public int DistritoID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
    }
}
