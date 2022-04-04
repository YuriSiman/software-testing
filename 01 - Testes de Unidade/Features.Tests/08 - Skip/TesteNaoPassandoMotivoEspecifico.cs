using Xunit;

namespace Features.Tests
{
    public class TesteNaoPassandoMotivoEspecifico
    {
        [Fact(DisplayName = "Novo cliente 2.0", Skip = "Nova versão 2.0 quebrando")]
        [Trait("01 - Testes de Unidade", "Escapando dos Testes")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }
    }
}
