using Lavanderia.Models;
using System;
using System.Web.Mvc;
using System.Configuration;

namespace Lavanderia.Controllers
{
    public class RolController : Controller
    {
        private readonly RolRepository _repo;

        public RolController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            _repo = new RolRepository(connectionString);
        }

        public ActionResult Index(string filtro = "")
        {
            var roles = string.IsNullOrWhiteSpace(filtro)
                ? _repo.ObtenerTodos()
                : _repo.Buscar(filtro);

            return View(roles);
        }

        public ActionResult Create()
        {
            return View(new Rol());
        }

        [HttpPost]
        public ActionResult Create(Rol rol)
        {
            if (!ModelState.IsValid)
                return View(rol);

            _repo.Registrar(rol);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var rol = _repo.ObtenerPorId(id);
            if (rol == null) return HttpNotFound();

            return View(rol);
        }

        [HttpPost]
        public ActionResult Edit(Rol rol)
        {
            if (!ModelState.IsValid)
                return View(rol);

            _repo.Actualizar(rol);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EliminarRolAjax(int id)
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
        public ActionResult HabilitarRolAjax(int id)
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
