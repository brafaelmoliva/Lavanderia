using System;

namespace Lavanderia.Models
{
    public class VistaDetalleBoleta
    {
        public int BoletaID { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntrega { get; set; }

        // Cliente
        public string NombreCliente { get; set; }
        public string DNICliente { get; set; }
        public string Telefono { get; set; }

        // Empleado
        public string AtendidoPor { get; set; }

        // Lavandería
        public string NombreLavanderia { get; set; }
        public string RUC { get; set; }
        public string DireccionLavanderia { get; set; }
        public string Distrito { get; set; }

        // Detalles
        public string Prenda { get; set; }
        public string TipoCobro { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Peso { get; set; }
        public decimal Subtotal { get; set; }

        // Totales
        public decimal Total { get; set; }
        public decimal IGV { get; set; }
        public decimal TotalConIGV { get; set; }
    }
}
