using Dapper;
using Lavanderia.Models;          
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class LoginController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"
                    SELECT u.UsuarioID, u.CorreoElectronico, u.Clave, u.Numero, u.EmpleadoID, u.RolID, u.Estado,
                           r.RolID, r.NombreRol, r.Descripcion, r.Estado
                    FROM Usuario u
                    INNER JOIN Rol r ON u.RolID = r.RolID
                    WHERE u.CorreoElectronico = @correo AND u.Clave = @clave AND u.Estado = 1 AND r.Estado = 1";

                var usuario = connection.Query<Usuario, Rol, Usuario>(
                    sql,
                    (u, r) => { u.Rol = r; return u; },
                    new { correo, clave },
                    splitOn: "RolID"
                ).FirstOrDefault();

                if (usuario != null)
                {
                    Session["UsuarioID"] = usuario.UsuarioID;
                    Session["CorreoElectronico"] = usuario.CorreoElectronico;
                    Session["Rol"] = usuario.Rol.NombreRol;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Correo o contraseña incorrectos.";
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
