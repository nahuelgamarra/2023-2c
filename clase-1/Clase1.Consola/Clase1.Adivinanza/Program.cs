namespace Clase1.Adivinanza
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escriba un numero maximo para jugar !");
            int numeroMaximo;
            bool estado = Int32.TryParse(Console.ReadLine(), out numeroMaximo);
            Console.WriteLine($"Su numero fue { numeroMaximo}");

            JuegoAdivinanza juego = new JuegoAdivinanza(numeroMaximo);
            Console.WriteLine("Escribi el numero, veremos si adivinaste");

            int numeroElegido;
            Int32.TryParse(Console.ReadLine(), out numeroElegido);

            juego.AdivinarNumero(numeroElegido);




        }
    }
}