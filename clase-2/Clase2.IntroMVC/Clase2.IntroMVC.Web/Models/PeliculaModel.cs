using Clase2.IntroMvc.Logica;
using Clase2.IntroMVC.Entidades;

namespace Clase2.IntroMVC.Web.Models;

public interface IPeliculaModel
{
    List<Pelicula> ObtenerTodas();
    Pelicula ObtenerPorId(int id);
    void AgregarPelicula(Pelicula pelicula);
    void EliminarPelicula(int id);

}

public class PeliculaModel : IPeliculaModel
{

    private IPeliculaRepositorio _peliculaRepositorio;
    public PeliculaModel(IPeliculaRepositorio peliculasRepositorio)
    {
        _peliculaRepositorio = peliculasRepositorio;
    }

    public void AgregarPelicula(Pelicula pelicula)
    {
        Console.WriteLine(pelicula.Genero);
        _peliculaRepositorio.Agregar(pelicula);
    }

    public void EliminarPelicula(int id)
    {
        try
        {
            _peliculaRepositorio.ObtenerPorId(id);
            _peliculaRepositorio.Eliminar(id);
        }
        catch (Exception)
        {

            throw new Exception("No se pudo eliminar la pelicula ");
        }
       
    }

    public Pelicula ObtenerPorId(int id)
    {
        Pelicula pelicula = _peliculaRepositorio.ObtenerPorId(id);
        return pelicula == null ? throw new Exception("No se pudo encontrar la pelicula") : pelicula;
    }

    public List<Pelicula> ObtenerTodas()
    {
        List<Pelicula> litsaPeliculas = _peliculaRepositorio.ObtenerTodas();
        return litsaPeliculas == null ? throw new Exception("No hay peliculas cargadas") : litsaPeliculas;
    }
}

