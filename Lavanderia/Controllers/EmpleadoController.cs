using Lavanderia.Models;
using System;
using System.Web.Mvc;
using System.Configuration;

namespace Lavanderia.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoRepository _repo;
        private readonly SucursalRepository _sucursalRepo;

        public EmpleadoController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            _repo = new EmpleadoRepository(connectionString);
            _sucursalRepo = new SucursalRepository(connectionString);
        }

        private void CargarSucursales(int? sucursalIdSeleccionada = null)
        {
            var sucursales = _sucursalRepo.ObtenerTodos();
            ViewBag.Sucursales = new SelectList(sucursales, "SucursalID", "Nombre", sucursalIdSeleccionada);
        }

        public ActionResult Index(string filtro = "")
        {
            var empleados = string.IsNullOrWhiteSpace(filtro)
                ? _repo.ObtenerTodos()
                : _repo.Buscar(filtro);

            return View(empleados);
        }

        public ActionResult Create()
        {
            CargarSucursales();
            return View(new Empleado());
        }

        [HttpPost]
        public ActionResult Create(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                CargarSucursales(empleado.SucursalID);
                return View(empleado);
            }

            _repo.Registrar(empleado);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var empleado = _repo.ObtenerPorId(id);
            if (empleado == null) return HttpNotFound();

            CargarSucursales(empleado.SucursalID);
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Edit(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                CargarSucursales(empleado.SucursalID);
                return View(empleado);
            }

            _repo.Actualizar(empleado);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EliminarEmpleadoAjax(int id)
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
        public ActionResult HabilitarEmpleadoAjax(int id)
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
