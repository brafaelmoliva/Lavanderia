using Lavanderia.Models;
using System;
using System.Web.Mvc;
using System.Configuration;

namespace Lavanderia.Controllers
{
    public class SucursalController : Controller
    {
        private readonly SucursalRepository _repo;
        private readonly DistritoRepository _distritoRepo;

        public SucursalController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            _repo = new SucursalRepository(connectionString);
            _distritoRepo = new DistritoRepository(connectionString);
        }

        private void CargarDistritos(int? distritoIdSeleccionado = null)
        {
            var distritos = _distritoRepo.ObtenerTodos();
            ViewBag.Distritos = new SelectList(distritos, "DistritoID", "NombreDistrito", distritoIdSeleccionado);
        }

        public ActionResult Index(string filtro = "")
        {
            var sucursales = string.IsNullOrWhiteSpace(filtro)
                ? _repo.ObtenerTodos()
                : _repo.Buscar(filtro);

            return View(sucursales);
        }

        public ActionResult Create()
        {
            CargarDistritos();
            return View(new Sucursal());
        }

        [HttpPost]
        public ActionResult Create(Sucursal sucursal)
        {
            if (!ModelState.IsValid)
            {
                CargarDistritos(sucursal.DistritoID);
                return View(sucursal);
            }

            _repo.Registrar(sucursal);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var sucursal = _repo.ObtenerPorId(id);
            if (sucursal == null) return HttpNotFound();

            CargarDistritos(sucursal.DistritoID);
            return View(sucursal);
        }

        [HttpPost]
        public ActionResult Edit(Sucursal sucursal)
        {
            if (!ModelState.IsValid)
            {
                CargarDistritos(sucursal.DistritoID);
                return View(sucursal);
            }

            _repo.Actualizar(sucursal);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EliminarSucursalAjax(int id)
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
        public ActionResult HabilitarSucursalAjax(int id)
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
