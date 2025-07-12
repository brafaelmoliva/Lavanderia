using Dapper;
using Lavanderia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class RolRepository
{
    private readonly string connectionString;

    public RolRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Rol> ObtenerTodos()
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Rol>("MostrarTodosRoles", commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Rol> Buscar(string filtro)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Rol>("MostrarRolPorFiltro",
                new { Filtro = filtro },
                commandType: CommandType.StoredProcedure);
    }

    public Rol ObtenerPorId(int rolId)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.QueryFirstOrDefault<Rol>(
                "MostrarRolPorId",
                new { RolID = rolId },
                commandType: CommandType.StoredProcedure);
    }

    public void Registrar(Rol rol)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("RegistrarRol", new
            {
                NombreRol = rol.NombreRol,
                Descripcion = rol.Descripcion
            }, commandType: CommandType.StoredProcedure);
    }

    public void Actualizar(Rol rol)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("ActualizarRol", new
            {
                RolID = rol.RolID,
                NombreRol = rol.NombreRol,
                Descripcion = rol.Descripcion,
                Estado = rol.Estado
            }, commandType: CommandType.StoredProcedure);
    }

    public void Eliminar(int rolId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("EliminarRol", new { RolID = rolId }, commandType: CommandType.StoredProcedure);
    }

    public void Habilitar(int rolId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("HabilitarRol", new { RolID = rolId }, commandType: CommandType.StoredProcedure);
    }
}
