using LeilaoOnline.Core;
using LeilaoOnline.Core.Interfaces;
using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornarMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            // Arrange - Given
            // Dado o leilão com lances sem ordem de valor
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

            // Act - Then
            // Quando o pregão termina
            leilao.TerminarPregao();

            // Assert - When
            // Então o valor esperado é o maior valor
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            // Arrange - Given
            // Dado o leilão
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            // Assert
            var exceptionObtida = Assert.Throws<InvalidOperationException>(
                // Act - Then
                // Quando o pregão termina
                () => leilao.TerminarPregao()
            );

            var msgEsperada = "Não é possível terminar o pregão sem que ele tenha começado.Para isso, utilize o método IniciarPregao()";
            Assert.Equal(msgEsperada, exceptionObtida.Message);
        }

        [Fact]
        public void RetornarZeroDadoLeilaoSemLances()
        {
            // Arrange - Given
            // Dado o leilão sem nenhum lance
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciarPregao();

            // Act - Then
            // Quando o pregão termina
            leilao.TerminarPregao();

            // Assert - When
            // Então o valor do lance ganhador é zero
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            // Arrange - Given
            // Dado o leilão iniciado coma  modalidade de avaliação "valor superior mais próximo", valor de destino 1200 e lances de 800, 1150, 1400 e 1250
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Cliente("Fulano", leilao);
            var maria = new Cliente("Maria", leilao);

            leilao.IniciarPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.ReceberLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.ReceberLance(maria, ofertas[i]);
                }
            }

            // Act - Then
            // Quando o pregão termina
            leilao.TerminarPregao();

            // Assert - When
            // Então o ganhador será quem fez a oferta 1250
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }
    }
}
