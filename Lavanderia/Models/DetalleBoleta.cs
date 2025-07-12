namespace Lavanderia.Models
{
    public class DetalleBoleta
    {
        public string PrendaNombre { get; set; }
        public string TipoCobro { get; set; }  // "kilo" o "unidad"
        public decimal Peso { get; set; }
        public decimal Precio { get; set; }
    }
}
