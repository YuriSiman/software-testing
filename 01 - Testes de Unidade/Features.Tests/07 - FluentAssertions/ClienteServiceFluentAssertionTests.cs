using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssertionTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssertionTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
            _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.True(cliente.EhValido());
            //_clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            //_clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);

            // Assert - Fluent Assertion
            cliente.EhValido().Should().BeTrue();

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.False(cliente.EhValido());
            //_clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            //_clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);

            // Assert - Fluent Assertion
            cliente.EhValido().Should().BeFalse("possui inconsistências");
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(1);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_ObterTotosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert
            //_clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            //Assert.True(clientes.Any());
            //Assert.False(clientes.Count(c => !c.Ativo) > 0);

            // Assert - Fluent Assertion
            clientes.Should().HaveCountGreaterThanOrEqualTo(1).And.OnlyHaveUniqueItems();
            clientes.Should().NotContain(c => !c.Ativo);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);

            // Este teste seria mais apropriado dentro de um Teste de Integração ou em um outro teste com um foco em performance
            _clienteService.ExecutionTimeOf(c => c.ObterTodosAtivos()).Should().BeLessThanOrEqualTo(50.Milliseconds(), "é executado milhares de vezes por segundo");
        }
    }
}
