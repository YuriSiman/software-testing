using System;

namespace LeilaoOnline.Core
{
    public class Lance
    {
        public Cliente Cliente { get; }
        public double Valor { get; }

        public Lance(Cliente cliente, double valor)
        {
            if (valor < 0) throw new ArgumentException("Valor do lance deve ser igual ou maior que zero");

            Cliente = cliente;
            Valor = valor;
        }
    }
}
