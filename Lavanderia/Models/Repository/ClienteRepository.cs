using Dapper;
using Lavanderia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class ClienteRepository
{
    private readonly string connectionString;

    public ClienteRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Cliente> ObtenerTodos()
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Cliente>("MostrarTodosClientes", commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Cliente> Buscar(string filtro)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.Query<Cliente>("MostrarClientePorCodigo",
            new { Filtro = filtro }, commandType: CommandType.StoredProcedure);
    }

    public Cliente ObtenerPorId(int clienteId)
    {
        using (var conexion = new SqlConnection(connectionString))
            return conexion.QueryFirstOrDefault<Cliente>(
            "MostrarClientePorCodigo",
            new { Filtro = clienteId.ToString() },
            commandType: CommandType.StoredProcedure);
    }

    public void Registrar(Cliente cliente)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("RegistrarCliente", new
        {
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            DNI = cliente.DNI,
            Numero = cliente.Numero
        }, commandType: CommandType.StoredProcedure);
    }

    public void Actualizar(Cliente cliente)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("ActualizarCliente", new
        {
            ClienteID = cliente.ClienteID,
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            DNI = cliente.DNI,
            Numero = cliente.Numero,
            Estado = cliente.Estado
        }, commandType: CommandType.StoredProcedure);
    }

    public void Eliminar(int clienteId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("EliminarCliente", new { ClienteID = clienteId }, commandType: CommandType.StoredProcedure);
    }

    public void Habilitar(int clienteId)
    {
        using (var conexion = new SqlConnection(connectionString))
            conexion.Execute("HabilitarCliente", new { ClienteID = clienteId }, commandType: CommandType.StoredProcedure);
    }
}
