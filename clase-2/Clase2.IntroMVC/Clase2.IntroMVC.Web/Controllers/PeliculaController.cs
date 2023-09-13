using Clase2.IntroMVC.Entidades;
using Clase2.IntroMVC.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Clase2.IntroMVC.Web.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly IPeliculaModel _model;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PeliculaController(IPeliculaModel model, IWebHostEnvironment hostingEnvironment)
        {
            _model = model;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Listado()
        {
            List<Pelicula> listado = _model.ObtenerTodas();
            return View(listado);
        }

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

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Pelicula pelicula, IFormFile Imagen)
        {
            try
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    if (!EsExtensionValida(Imagen))
                    {
                        ModelState.AddModelError("Imagen", "La extensión de la imagen no es válida. Debe ser png, jpg, jpeg o gif.");
                        return View(pelicula);
                    }

                    GuardarImagen(pelicula, Imagen);
                }

                SetFechaEstreno(pelicula);

                pelicula.NominadaAlOsccar = Request.Form["NominadaAlOscar"] == "on";

                _model.AgregarPelicula(pelicula);

                return RedirectToAction("Listado");
            }
            catch
            {
                return RedirectToAction("Agregar", pelicula);
            }
        }
        public IActionResult Eliminar(int id) {
           
            try
            {
                _model.EliminarPelicula(id);
                return RedirectToAction("Listado");
            }
            catch
            {
                  return RedirectToAction("Listado");
            }

        }

        public IActionResult Actualizar(int id) {
            try
            {
                var pelicula = _model.ObtenerPorId(id);
              
                return View(pelicula);
            }
            catch (Exception)
            {
                return View(null);  
            }
        }
       


       [HttpPost]
        public IActionResult ActualizarPelicula(Pelicula pelicula,  IFormFile Imagen)
        {
            try {
                var peliculaExistente= _model.ObtenerPorId(pelicula.Id);

                if (Imagen != null && Imagen.Length > 0)
                {
                    if (!EsExtensionValida(Imagen))
                    {
                        ModelState.AddModelError("Imagen", "La extensión de la imagen no es válida. Debe ser png, jpg, jpeg o gif.");
                        return View(pelicula);
                    }

                    GuardarImagen(peliculaExistente, Imagen);
                }
                SetFechaEstreno(pelicula);
                peliculaExistente.FechaEstreno= pelicula.FechaEstreno;
                peliculaExistente.Titulo= pelicula.Titulo;
                peliculaExistente.Genero= pelicula.Genero;
                peliculaExistente.Recaudacion = pelicula.Recaudacion;
                peliculaExistente.Presupuesto= pelicula.Presupuesto;
                peliculaExistente.NominadaAlOsccar = Request.Form["NominadaAlOscar"] == "on";
                _model.ActulizarPelicula(peliculaExistente);
                return RedirectToAction("Listado");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Listado");
            }

        }
        private void SetFechaEstreno(Pelicula pelicula)
        {
            if (DateOnly.TryParse(Request.Form["FechaEstreno"], out DateOnly fechaEstreno))
            {
                pelicula.FechaEstreno = fechaEstreno;
            }
        }
        private bool EsExtensionValida(IFormFile imagen)
        {
            var extension = Path.GetExtension(imagen.FileName).ToLower();
            var extensionesPermitidas = new[] { ".png", ".jpg", ".jpeg", ".gif" };
            return extensionesPermitidas.Contains(extension);
        }

        private void GuardarImagen(Pelicula pelicula, IFormFile imagen)
        {
            var tituloLimpio = new string(pelicula.Titulo
                .Where(c => !char.IsWhiteSpace(c) && !Path.GetInvalidFileNameChars().Contains(c))
                .ToArray());

            var extension = Path.GetExtension(imagen.FileName).ToLower();
            var nombreArchivo = tituloLimpio + extension;
            var rutaDeCarpetaDondeGuardar = Path.Combine(_hostingEnvironment.WebRootPath, "images", "portada");
            var rutaDeGuardado = Path.Combine(rutaDeCarpetaDondeGuardar, nombreArchivo);

            using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
            {
                imagen.CopyTo(stream);
            }

            pelicula.Imagen = nombreArchivo;
        }
    }
}
