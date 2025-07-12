using Dapper;
using Lavanderia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class SucursalRepository
{
    private readonly string connectionString;

    public SucursalRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Sucursal> ObtenerTodos()
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Sucursal>("MostrarTodosSucursales", commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Sucursal> Buscar(string filtro)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Sucursal>("MostrarSucursalPorFiltro",
                new { Filtro = filtro },
                commandType: CommandType.StoredProcedure);
    }

    public Sucursal ObtenerPorId(int sucursalId)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.QueryFirstOrDefault<Sucursal>(
                "MostrarSucursalPorId",
                new { SucursalID = sucursalId },
                commandType: CommandType.StoredProcedure);
    }

    public void Registrar(Sucursal sucursal)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("RegistrarSucursal", new
            {
                DistritoID = sucursal.DistritoID,
                Nombre = sucursal.Nombre,
                Direccion = sucursal.Direccion
            }, commandType: CommandType.StoredProcedure);
    }

    public void Actualizar(Sucursal sucursal)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("ActualizarSucursal", new
            {
                SucursalID = sucursal.SucursalID,
                DistritoID = sucursal.DistritoID,
                Nombre = sucursal.Nombre,
                Direccion = sucursal.Direccion,
                Estado = sucursal.Estado
            }, commandType: CommandType.StoredProcedure);
    }

    public void Eliminar(int sucursalId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("EliminarSucursal", new { SucursalID = sucursalId }, commandType: CommandType.StoredProcedure);
    }

    public void Habilitar(int sucursalId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("HabilitarSucursal", new { SucursalID = sucursalId }, commandType: CommandType.StoredProcedure);
    }
}
