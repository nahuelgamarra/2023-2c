
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace Clase2.IntroMVC.Web.Models
{
    public class PeliculaViewModel
    {
        [Required(ErrorMessage = "El titulo de la pelicula es requido")]
        [StringLength(100, ErrorMessage ="El titulo no puede tener mas de 100 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El genero de la pelicula es requido")]
        [StringLength(50, ErrorMessage = "El genero no puede tener mas de 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La fecha de estreno es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Estreno")]
        public DateTime FechaEstreno { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto debe ser mayor que cero")]
        public double Presupuesto {get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto debe ser mayor que cero")]
        public double Recaudacion { get; set; }
        


        public bool NominadaAlOscar { get; set; }
       

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "La imagen es requerida")]
        public IFormFile Imagen { get; set; }

        // Validación personalizada para la extensión del archivo
        public bool EsExtensionValida()
        {
            if (Imagen != null)
            {
                string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif" }; 
                var extension = Path.GetExtension(Imagen.FileName).ToLower();

                if (!extensionesPermitidas.Contains(extension))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
