using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clase2.IntroMVC.Entidades;

namespace Clase2.IntroMvc.Logica;
public interface IPeliculaRepositorio
{
    List<Pelicula> ObtenerTodas();
    void Agregar(Pelicula pelicula);
    Pelicula? ObtenerPorId(int id);
    void Actualizar(Pelicula pelicula);
    void Eliminar(int id);
}
public class PeliculasRepositorio : IPeliculaRepositorio
{



    private static List<Pelicula> _peliculas = new List<Pelicula>();
    public void Actualizar(Pelicula pelicula)
    {
        throw new NotImplementedException();
    }

    public void Agregar(Pelicula pelicula)
    {

        pelicula.Id = _peliculas.Count + 1;
        _peliculas.Add(pelicula);
    }

    public void Eliminar(int id)
    {
        Pelicula pelicula = ObtenerPorId(id);
        if (pelicula == null)
        {
            throw new Exception("No se pudo eliminar");
        }
        _peliculas.Remove(pelicula);
    }

    public Pelicula? ObtenerPorId(int id)
    {
      return _peliculas.FirstOrDefault(p => p.Id == id);
    }

    public List<Pelicula> ObtenerTodas()
    {
        return _peliculas;
    }
}

