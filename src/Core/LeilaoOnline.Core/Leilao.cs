using LeilaoOnline.Core.Enums;
using LeilaoOnline.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeilaoOnline.Core
{
    public class Leilao
    {
        private Cliente _ultimoCliente;
        private IList<Lance> _lances;
        private IModalidadeAvaliacao _avaliador;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;
        }

        public void ReceberLance(Cliente cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciarPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminarPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado.Para isso, utilize o método IniciarPregao()");
            }

            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;
        }

        private bool NovoLanceEhAceito(Cliente cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento) && (cliente != _ultimoCliente);
        }
    }
}
