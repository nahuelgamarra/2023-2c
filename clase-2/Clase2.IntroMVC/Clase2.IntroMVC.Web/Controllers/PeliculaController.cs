using Clase2.IntroMVC.Entidades;
using Clase2.IntroMVC.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase2.IntroMVC.Web.Controllers
{
    public class PeliculaController : Controller
    {
        private IPeliculaModel _model;

        public PeliculaController(IPeliculaModel model)
        {
            _model = model;
        }
        // GET: PeliculaController
        public IActionResult Listado()
        {
            List<Pelicula> listado = _model.ObtenerTodas();
            return View(listado);
        }

        // GET: PeliculaController
        public IActionResult ObtenerPorId(int id)
        {
            Pelicula pelicula = null;
            try
            {
                pelicula = _model.ObtenerPorId(id);
                return RedirectToAction("Listado");
            }
            catch (Exception)
            {
                return RedirectToAction("Listado");
            }

        }

        // GET: PeliculaController/Create
        public IActionResult Agregar()
        {
            return View();
        }

        // POST: PeliculaController/Create
        [HttpPost]
        public IActionResult Agregar(Pelicula pelicula)
        {
            try
            {
                if (DateOnly.TryParse(Request.Form["FechaEstreno"], out DateOnly fechaEstreno))
                {
                    pelicula.FechaEstreno = fechaEstreno;
                }

                Console.WriteLine(Request.Form["FechaEstreno"]);

                _model.AgregarPelicula(pelicula);

                return RedirectToAction("Listado");
            }
            catch
            {
                return RedirectToAction("Listado");
            }
        }

        // GET: PeliculaController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: PeliculaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PeliculaController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: PeliculaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
