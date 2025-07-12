using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Lavanderia.Models.Repository
{
    public class UsuarioRepository
    {
        private readonly string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Usuario ValidarLogin(string correo, string clave)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = @"
                SELECT u.*, r.RolID, r.NombreRol, r.Descripcion, r.Estado
                FROM Usuario u
                INNER JOIN Rol r ON u.RolID = r.RolID
                WHERE u.CorreoElectronico = @correo AND u.Clave = @clave AND u.Estado = 1 AND r.Estado = 1";

                var usuario = connection.Query<Usuario, Rol, Usuario>(
                    sql,
                    (u, r) => { u.Rol = r; return u; },
                    new { correo, clave },
                    splitOn: "RolID"
                ).FirstOrDefault();

                return usuario;
            }
        }

        // Nuevo método: Obtener todos los usuarios (activos e inactivos)
        public IEnumerable<Usuario> ObtenerTodos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>("MostrarTodosUsuarios", commandType: CommandType.StoredProcedure);
            }
        }

        // Nuevo método: Buscar usuarios por filtro (correo o número)
        public IEnumerable<Usuario> Buscar(string filtro)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(
                    "MostrarUsuarioPorFiltro",
                    new { Filtro = filtro },
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Nuevo método: Obtener usuario por ID, con rol incluido
        public Usuario ObtenerPorId(int usuarioId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "MostrarUsuarioPorId";

                var usuario = connection.Query<Usuario, Rol, Usuario>(
                    sql,
                    (u, r) => { u.Rol = r; return u; },
                    new { UsuarioID = usuarioId },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "RolID"
                ).FirstOrDefault();

                return usuario;
            }
        }

        // Nuevo método: Registrar usuario
        public void Registrar(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    "RegistrarUsuario",
                    new
                    {
                        CorreoElectronico = usuario.CorreoElectronico,
                        Clave = usuario.Clave,
                        Numero = usuario.Numero,
                        EmpleadoID = usuario.EmpleadoID,
                        RolID = usuario.RolID
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Nuevo método: Actualizar usuario
        public void Actualizar(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    "ActualizarUsuario",
                    new
                    {
                        UsuarioID = usuario.UsuarioID,
                        CorreoElectronico = usuario.CorreoElectronico,
                        Clave = usuario.Clave,
                        Numero = usuario.Numero,
                        EmpleadoID = usuario.EmpleadoID,
                        RolID = usuario.RolID,
                        Estado = usuario.Estado
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Nuevo método: Eliminar usuario (lógico)
        public void Eliminar(int usuarioId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    "EliminarUsuario",
                    new { UsuarioID = usuarioId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Nuevo método: Habilitar usuario
        public void Habilitar(int usuarioId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    "HabilitarUsuario",
                    new { UsuarioID = usuarioId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
