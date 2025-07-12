using Dapper;
using Lavanderia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class EmpleadoRepository
{
    private readonly string connectionString;

    public EmpleadoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Empleado> ObtenerTodos()
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Empleado>("MostrarTodosEmpleados", commandType: CommandType.StoredProcedure);
    }

    public Empleado ObtenerPorId(int empleadoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.QueryFirstOrDefault<Empleado>(
                "MostrarEmpleadoPorId",
                new { EmpleadoID = empleadoId },
                commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Empleado> Buscar(string filtro)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Empleado>("MostrarEmpleadoPorCodigo",
                new { Filtro = filtro },
                commandType: CommandType.StoredProcedure);
    }

    public void Registrar(Empleado empleado)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("RegistrarEmpleado", new
            {
                SucursalID = empleado.SucursalID,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                DNI = empleado.DNI,
                Numero = empleado.Numero,
                HoraEntrada = empleado.HoraEntrada,
                HoraSalida = empleado.HoraSalida
            }, commandType: CommandType.StoredProcedure);
    }

    public void Actualizar(Empleado empleado)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("ActualizarEmpleado", new
            {
                EmpleadoID = empleado.EmpleadoID,
                SucursalID = empleado.SucursalID,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                DNI = empleado.DNI,
                Numero = empleado.Numero,
                HoraEntrada = empleado.HoraEntrada,
                HoraSalida = empleado.HoraSalida,
                Estado = empleado.Estado
            }, commandType: CommandType.StoredProcedure);
    }

    public void Eliminar(int empleadoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("EliminarEmpleado", new { EmpleadoID = empleadoId }, commandType: CommandType.StoredProcedure);
    }

    public void Habilitar(int empleadoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("HabilitarEmpleado", new { EmpleadoID = empleadoId }, commandType: CommandType.StoredProcedure);
    }
}
