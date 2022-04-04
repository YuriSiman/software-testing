using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteBogusTests
    {
        private readonly ClienteTestsBogusFixture _clienteTestsBogusFixture;

        public ClienteBogusTests(ClienteTestsBogusFixture clienteTestsBogusFixture)
        {
            _clienteTestsBogusFixture = clienteTestsBogusFixture;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_EhValido_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_EhValido_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Novo Cliente é Especial")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_EhEspecial_DeveSerEspecial()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValidoEspecial();

            // Act
            var result = cliente.EhEspecial();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Novo Cliente Não é Especial")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_EhEspecial_NaoDeveSerEspecial()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();

            // Act
            var result = cliente.EhEspecial();

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Cliente Inativado")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_Inativar_DeveSerInativado()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();

            // Act
            cliente.Inativar();

            // Assert
            Assert.False(cliente.Ativo);
        }

        [Fact(DisplayName = "Cliente Possui Nome Completo")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_NomeCompleto_DevePossuirNomeCompleto()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();

            // Act
            var result = cliente.NomeCompleto();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Cliente Não Possui Nome Completo")]
        [Trait("Categoria", "Cliente Bogus Tests")]
        public void Cliente_NomeCompleto_NaoDevePossuirNomeCompleto()
        {
            // Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteInvalido();

            // Act
            var result = cliente.NomeCompleto();

            // Assert
            Assert.False(result);
        }
    }
}
