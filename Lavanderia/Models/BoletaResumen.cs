using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lavanderia.Models
{
    public class BoletaResumen
    {
        public int BoletaID { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string NombreCliente { get; set; }
        public string NombreEmpleado { get; set; }
        public string NombreSucursal { get; set; }
        public decimal Total { get; set; }
    }

}