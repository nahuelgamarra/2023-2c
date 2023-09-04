using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase1.Adivinanza
{
    public class JuegoAdivinanza
    {

        private int numeroParaAdivinar;
        private int numeroElegido;
        private bool acertado;
        private int intento;

        public JuegoAdivinanza(int numero)
        {

            this.numeroParaAdivinar = new Random().Next(1, numero);
            this.acertado = false;
            this.intento = 0;

        }

        public bool AdivinarNumero(int numero)
        {
            if (numeroParaAdivinar == numero)
            {
                acertado = true;
            }

            Console.WriteLine("Numero de la maquina" + numeroParaAdivinar);
            Console.WriteLine("Numero tuyo " + numero);
            Console.WriteLine(acertado);
            return acertado;
        }

        
    }
}
