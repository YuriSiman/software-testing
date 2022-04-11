using AngleSharp.Html.Parser;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class PedidoTests
    {
        private readonly IntegrationTestsFixture _testsFixture;

        public PedidoTests(IntegrationTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar item em novo pedido")]
        [Trait("03 - Testes de Integração", "Integração Web - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveAtualizarValorTotal()
        {
            // Arrange 

            var produtoId = new Guid("edfccd43-bdbd-4e56-b28d-53171cf8770d");
            const int quantidade = 2;

            var initialResponse = await _testsFixture.Client.GetAsync($"/produto-detalhe/{produtoId}");
            initialResponse.EnsureSuccessStatusCode();

            var formData = new Dictionary<string, string>
            {
                {"Id", produtoId.ToString() },
                {"quantidade", quantidade.ToString() }
            };

            await _testsFixture.RealizarLoginWeb();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/meu-carrinho")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            postResponse.EnsureSuccessStatusCode();

            var html = new HtmlParser().ParseDocumentAsync(await postRequest.Content.ReadAsStringAsync()).Result.All;

            var formQuantidade = html?.FirstOrDefault(c => c.Id == "quantidade")?.GetAttribute("value")?.ApenasNumeros();
            var valorUnitario = html?.FirstOrDefault(c => c.Id == "valorUnitario")?.TextContent.Split(".")[0]?.ApenasNumeros();
            var valorTotal = html?.FirstOrDefault(c => c.Id == "valorTotal")?.TextContent.Split(".")[0]?.ApenasNumeros();

            Assert.Equal(valorTotal, valorUnitario * formQuantidade);
        }
    }
}
