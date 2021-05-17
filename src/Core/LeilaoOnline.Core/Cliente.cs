using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoOnline.Core
{
    public class Cliente
    {
        public string Nome { get; }
        public Leilao Leilao { get; }

        public Cliente(string nome, Leilao leilao)
        {
            Nome = nome;
            Leilao = leilao;
        }
    }
}
