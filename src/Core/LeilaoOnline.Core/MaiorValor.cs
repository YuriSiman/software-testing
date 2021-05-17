using LeilaoOnline.Core.Interfaces;
using System.Linq;

namespace LeilaoOnline.Core
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                   .DefaultIfEmpty(new Lance(null, 0))
                   .OrderBy(l => l.Valor)
                   .LastOrDefault();
        }
    }
}
