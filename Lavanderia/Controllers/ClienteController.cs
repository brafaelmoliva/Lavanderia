using Lavanderia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepository _repo;

        public ClienteController()
        {
            // Cambia la cadena por la tuya
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            _repo = new ClienteRepository(connectionString);
        }

        public ActionResult Index(string filtro = "")
        {
            var clientes = string.IsNullOrWhiteSpace(filtro)
                ? _repo.ObtenerTodos()
                : _repo.Buscar(filtro);

            return View(clientes);
        }

        public ActionResult Create()
        {
            return View(new Cliente());
        }


        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);

            _repo.Registrar(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var cliente = _repo.ObtenerPorId(id);
            if (cliente == null) return HttpNotFound();

            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);

            _repo.Actualizar(cliente);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EliminarClienteAjax(int id)
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
        public ActionResult HabilitarClienteAjax(int id)
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