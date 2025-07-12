using Lavanderia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class BoletaController : Controller
    {
        private readonly BoletaRepository _repo;
        private readonly ClienteRepository _clienteRepo;
        private readonly EmpleadoRepository _empleadoRepo;
        private readonly SucursalRepository _sucursalRepo;

        // Constructor: inicializa todos los repositorios necesarios
        public BoletaController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            _repo = new BoletaRepository(connectionString);
            _clienteRepo = new ClienteRepository(connectionString);
            _empleadoRepo = new EmpleadoRepository(connectionString);
            _sucursalRepo = new SucursalRepository(connectionString);
        }

        // Método para cargar los datos de los combos (dropdowns)
        private void CargarCombos()
        {
            var clientes = _clienteRepo.ObtenerTodos();
            var empleados = _empleadoRepo.ObtenerTodos();
            var sucursales = _sucursalRepo.ObtenerTodos();

            // Se pasan como ViewBag para usarlos en la vista con SelectList
            ViewBag.Clientes = new SelectList(clientes, "ClienteID", "NombreCompleto");
            ViewBag.Empleados = new SelectList(empleados, "EmpleadoID", "NombreCompleto");
            ViewBag.Sucursales = new SelectList(sucursales, "SucursalID", "Nombre");
        }

        // GET: Boleta/Create - Carga la vista inicial del formulario con un detalle por defecto
        public ActionResult Create()
        {
            CargarCombos(); // Llenar los dropdowns

            return View(new Boleta
            {
                FechaEmision = DateTime.Today,
                FechaEntrega = DateTime.Today.AddDays(3),
                Detalles = new List<DetalleBoleta>
                {
                    new DetalleBoleta() // Primer detalle vacío
                }
            });
        }

        // POST: Boleta/Create - Envía la boleta y sus detalles al SP
        [HttpPost]
        public ActionResult Create(Boleta boleta)
        {
            // Validar que haya al menos un detalle
            if (boleta.Detalles == null || boleta.Detalles.Count == 0)
            {
                ModelState.AddModelError("", "Debe ingresar al menos un detalle.");
                CargarCombos(); // Volver a cargar combos en caso de error
                return View(boleta);
            }

            // Imprimir detalles en Debug (útil para desarrollo)
            foreach (var d in boleta.Detalles)
            {
                System.Diagnostics.Debug.WriteLine($"Detalle: {d.PrendaNombre} - {d.TipoCobro} - {d.Peso} - {d.Precio}");
            }

            // Registrar la boleta con los detalles
            _repo.RegistrarBoletaConDetalles(boleta);

            // Redirigir tras registro exitoso
            return RedirectToAction("Index", "Home");
        }
    }
}
