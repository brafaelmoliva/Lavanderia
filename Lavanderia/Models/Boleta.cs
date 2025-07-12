using System;
using System.Collections.Generic;

namespace Lavanderia.Models
{
    public class Boleta
    {
        public int BoletaID { get; set; }  // Solo para devolverlo
        public int ClienteID { get; set; }
        public int EmpleadoID { get; set; }
        public int SucursalID { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntrega { get; set; }
        public List<DetalleBoleta> Detalles { get; set; }
    }
}
