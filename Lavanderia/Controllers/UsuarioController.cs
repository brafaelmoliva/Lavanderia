using Lavanderia.Models;
using Lavanderia.Models.Repository;
using System;
using System.Web.Mvc;
using System.Configuration;

namespace Lavanderia.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repo;

        public UsuarioController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            _repo = new UsuarioRepository(connectionString);
        }

        public ActionResult Index(string filtro = "")
        {
            var usuarios = string.IsNullOrWhiteSpace(filtro)
                ? _repo.ObtenerTodos()
                : _repo.Buscar(filtro);

            return View(usuarios);
        }

       public ActionResult Create()
{
    // Cargar lista de roles para el dropdown
    var rolRepo = new RolRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
    var roles = rolRepo.ObtenerTodos();
    ViewBag.Roles = new SelectList(roles, "RolID", "NombreRol");

    // (Opcional) si quieres también empleados en Create
    var empleadoRepo = new EmpleadoRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
    var empleados = empleadoRepo.ObtenerTodos();
    ViewBag.Empleados = new SelectList(empleados, "EmpleadoID", "Apellido");

    return View(new Usuario());
}

[HttpPost]
public ActionResult Create(Usuario usuario)
{
    if (!ModelState.IsValid)
    {
        // Volver a cargar dropdowns antes de devolver la vista
        var rolRepo = new RolRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        var roles = rolRepo.ObtenerTodos();
        ViewBag.Roles = new SelectList(roles, "RolID", "NombreRol", usuario.RolID);

        var empleadoRepo = new EmpleadoRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        var empleados = empleadoRepo.ObtenerTodos();
        ViewBag.Empleados = new SelectList(empleados, "EmpleadoID", "NombreCompleto", usuario.EmpleadoID);

        return View(usuario);
    }

    _repo.Registrar(usuario);
    return RedirectToAction("Index");
}

        public ActionResult Edit(int id)
        {
            var usuario = _repo.ObtenerPorId(id);
            if (usuario == null) return HttpNotFound();

            // Cargar lista de roles
            var rolRepo = new RolRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
            var roles = rolRepo.ObtenerTodos();
            ViewBag.Roles = new SelectList(roles, "RolID", "NombreRol", usuario.RolID);

            // Cargar lista de empleados
            var empleadoRepo = new EmpleadoRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
            var empleados = empleadoRepo.ObtenerTodos();
            ViewBag.Empleados = new SelectList(empleados, "EmpleadoID", "Apellido", usuario.EmpleadoID);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                // Volver a cargar listas para dropdowns
                var rolRepo = new RolRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
                var roles = rolRepo.ObtenerTodos();
                ViewBag.Roles = new SelectList(roles, "RolID", "NombreRol", usuario.RolID);

                var empleadoRepo = new EmpleadoRepository(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
                var empleados = empleadoRepo.ObtenerTodos();
                ViewBag.Empleados = new SelectList(empleados, "EmpleadoID", "NombreCompleto", usuario.EmpleadoID);

                return View(usuario);
            }

            _repo.Actualizar(usuario);
            return RedirectToAction("Index");
        }



[HttpPost]
        public ActionResult EliminarUsuarioAjax(int id)
        {
            try
            {
                _repo.Eliminar(id);
                return new HttpStatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error al eliminar: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult HabilitarUsuarioAjax(int id)
        {
            try
            {
                _repo.Habilitar(id);
                return new HttpStatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error al habilitar: " + ex.Message);
            }
        }
    }
}
