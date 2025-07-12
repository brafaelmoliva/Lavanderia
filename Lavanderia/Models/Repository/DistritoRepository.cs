using Dapper;
using Lavanderia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class DistritoRepository
{
    private readonly string connectionString;

    public DistritoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Distrito> ObtenerTodos()
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Distrito>("MostrarTodosDistritos", commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Distrito> Buscar(string filtro)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Distrito>("MostrarDistritoPorFiltro",
                new { Filtro = filtro },
                commandType: CommandType.StoredProcedure);
    }

    public Distrito ObtenerPorCodigo(int distritoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.QueryFirstOrDefault<Distrito>(
                "MostrarDistritoPorId",
                new { DistritoID = distritoId },
                commandType: CommandType.StoredProcedure);
    }

    public Distrito ObtenerPorId(int distritoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.QueryFirstOrDefault<Distrito>(
                "MostrarDistritoPorFiltro",
                new { Filtro = distritoId.ToString() },
                commandType: CommandType.StoredProcedure);
    }

    public void Registrar(Distrito distrito)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("RegistrarDistrito", new
            {
                NombreDistrito = distrito.NombreDistrito
            }, commandType: CommandType.StoredProcedure);
    }

    public void Actualizar(Distrito distrito)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("ActualizarDistrito", new
            {
                DistritoID = distrito.DistritoID,
                NombreDistrito = distrito.NombreDistrito,
                Estado = distrito.Estado
            }, commandType: CommandType.StoredProcedure);
    }

    public void Eliminar(int distritoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("EliminarDistrito", new { DistritoID = distritoId }, commandType: CommandType.StoredProcedure);
    }

    public void Habilitar(int distritoId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("HabilitarDistrito", new { DistritoID = distritoId }, commandType: CommandType.StoredProcedure);
    }
}
