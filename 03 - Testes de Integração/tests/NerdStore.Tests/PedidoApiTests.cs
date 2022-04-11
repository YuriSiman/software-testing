using Features.Tests;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {
        private readonly IntegrationTestsFixture _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar item em novo pedido"), TestPriority(1)]
        [Trait("03 - Testes de Integração", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            // Arrange 
            var itemInfo = new ItemViewModel
            {
                Id = new Guid("edfccd43-bdbd-4e56-b28d-53171cf8770d"),
                Quantidade = 2
            };

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/carrinho", itemInfo);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover item em pedido existente"), TestPriority(2)]
        [Trait("03 - Testes de Integração", "Integração API - Pedido")]
        public async Task RemoverItem_PedidoExistente_DeveRetornarComSucesso()
        {
            // Arrange
            var produtoId = new Guid("edfccd43-bdbd-4e56-b28d-53171cf8770d");
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"api/carrinho/{produtoId}");

            //Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
