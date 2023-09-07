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
                    // Obtener la extensión del archivo de imagen
                    var extension = Path.GetExtension(Imagen.FileName).ToLower();

                    // Lista de extensiones permitidas (por ejemplo, png, jpg, jpeg, gif)
                    var extensionesPermitidas = new[] { ".png", ".jpg", ".jpeg", ".gif" };

                    if (!extensionesPermitidas.Contains(extension))
                    {
                        // La extensión no está permitida, puedes manejar el error o redirigir a una página de error
                        ModelState.AddModelError("Imagen", "La extensión de la imagen no es válida. Debe ser png, jpg, jpeg o gif.");
                        return View(pelicula);
                    }

                    // Cambiar el nombre del archivo de imagen al título de la película
                    var tituloLimpio = new string(pelicula.Titulo
                        .Where(c => !char.IsWhiteSpace(c) && !Path.GetInvalidFileNameChars().Contains(c))
                        .ToArray());

                    var nombreArchivo = tituloLimpio + extension;
                    var rutaDeCarpetaDondeGuardar = Path.Combine(_hostingEnvironment.WebRootPath, "images", "portada");
                    var rutaDeGuardado = Path.Combine(rutaDeCarpetaDondeGuardar, nombreArchivo);

                    using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
                    {
                        Imagen.CopyTo(stream);
                    }

                    // Asignar el nombre del archivo a la propiedad Imagen de la película
                    pelicula.Imagen = nombreArchivo;
                }

                if (DateOnly.TryParse(Request.Form["FechaEstreno"], out DateOnly fechaEstreno))
                {
                    pelicula.FechaEstreno = fechaEstreno;
                }

                pelicula.NominadaAlOsccar = Request.Form["NominadaAlOscar"] == "on";

                _model.AgregarPelicula(pelicula);

                return RedirectToAction("Listado");
            }
            catch
            {
                return RedirectToAction("Listado");
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

        public IActionResult ActualizarPelicula(Pelicula pelicula) 
        {

            return RedirectToAction("Listado");
        }


    }


}
