using LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int quantidadeEsperada, double[] ofertas)
        {
            // Arrange - Given
            // Dado leilão finalizado com x lances
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Cliente("Fulano", leilao);
            var maria = new Cliente("Maria", leilao);

            leilao.IniciarPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if (i % 2 == 0)
                {
                    leilao.ReceberLance(fulano, valor);
                }
                else
                {
                    leilao.ReceberLance(maria, valor);
                }
            }

            leilao.TerminarPregao();

            // Act - Then
            // Quando o leilão receber nova oferta de lance
            leilao.ReceberLance(fulano, 1000);

            // Assert - When
            // Então a quantidade de lances continua sendo x
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }

        [Fact]
        public void NaoAceitarProximoLanceDadoQueMesmoClienteRealizouUltimoLance()
        {
            // Arrange - Given
            // Dado o lielão iniciado e cliente x realizou o último lance
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Cliente("Fulano", leilao);

            leilao.IniciarPregao();
            leilao.ReceberLance(fulano, 800);

            // Act - Then
            // Quando o mesmo cliente x realiza o próximo lance
            leilao.ReceberLance(fulano, 1000);

            // Assert - When
            // Então o leilão não aceita o segundo lance
            var quantidadeEsperada = 1;
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }
    }
}
