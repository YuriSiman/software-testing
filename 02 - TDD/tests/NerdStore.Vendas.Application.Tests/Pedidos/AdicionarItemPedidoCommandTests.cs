using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Validation;
using NerdStore.Vendas.Domain;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Válido")]
        [Trait("02 - TDD", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        [Trait("02 - TDD", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Item Command Unidades Acima do Permitido")]
        [Trait("02 - TDD", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesSuperiorAoPermitido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            
        }
    }
}
