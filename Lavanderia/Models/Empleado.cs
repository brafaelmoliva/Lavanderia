using System;

namespace Lavanderia.Models
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public int SucursalID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Numero { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public bool Estado { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";

    }
}
