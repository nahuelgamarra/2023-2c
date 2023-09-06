using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase2.IntroMVC.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }    
        public string? Genero { get; set;}
        public DateOnly? FechaEstreno { get; set; }
        public double? Presupuesto { get; set; } 
        public double? Recaudacion { get; set;}
        public string? Imagen { get; set;}
        public bool? NominadaAlOsccar { get; set; }

    }
}
