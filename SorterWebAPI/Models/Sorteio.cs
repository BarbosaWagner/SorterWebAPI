using System;

namespace SorterWebAPI.Models
{
    public class Sorteio
    {
        private int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public int SortearNumero()
        {
            Random sortear = new Random();
            int numeroSorteado = sortear.Next(0, 10000);

            return numeroSorteado;
        }
    }
}