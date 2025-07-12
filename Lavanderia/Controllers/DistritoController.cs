using Lavanderia.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class DistritoController : Controller
    {
        private readonly DistritoRepository _repo;

        public DistritoController()
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            _repo = new DistritoRepository(cs);
        }

        public ActionResult Index(string filtro = "")
        {
            var list = string.IsNullOrWhiteSpace(filtro)
                ? _repo.ObtenerTodos()
                : _repo.Buscar(filtro);
            return View(list);
        }

        public ActionResult Create() => View(new Distrito());

        [HttpPost]
        public ActionResult Create(Distrito d)
        {
            if (!ModelState.IsValid) return View(d);
            _repo.Registrar(d);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var d = _repo.ObtenerPorCodigo(id);
            if (d == null) return HttpNotFound();
            return View(d);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Distrito d)
        {
            if (!ModelState.IsValid)
                return View(d);

            _repo.Actualizar(d);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult EliminarDistritoAjax(int id)  // <- Nombre exacto que espera la vista
        {
            try { _repo.Eliminar(id); return new HttpStatusCodeResult(200); }
            catch (Exception ex) { return new HttpStatusCodeResult(500, ex.Message); }
        }

        [HttpPost]
        public ActionResult HabilitarDistritoAjax(int id)  // <- También cambia este
        {
            try { _repo.Habilitar(id); return new HttpStatusCodeResult(200); }
            catch (Exception ex) { return new HttpStatusCodeResult(500, ex.Message); }
        }



    }
}
