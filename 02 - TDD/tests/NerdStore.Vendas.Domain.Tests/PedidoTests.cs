using NerdStore.Core.DomainObjects.Exceptions;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        private readonly Pedido _pedido;
        private readonly Guid _produtoId;

        public PedidoTests()
        {
            _pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            _produtoId = Guid.NewGuid();
        }

        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            _pedido.AdicionarItemPedido(pedidoItem);

            // Assert
            Assert.Equal(200, _pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            // Arrange
            var pedidoItem = new PedidoItem(_produtoId, "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(_produtoId, "Produto Teste", 1, 100);

            // Act
            _pedido.AdicionarItemPedido(pedidoItem);
            _pedido.AdicionarItemPedido(pedidoItem2);

            // Assert
            Assert.Equal(300, _pedido.ValorTotal);
            Assert.Equal(1, _pedido.PedidoItens.Count);
            Assert.Equal(3, _pedido.PedidoItens.FirstOrDefault(p => p.ProdutoId == _produtoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima do Permitido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_UnidadesItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedidoItem = new PedidoItem(_produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _pedido.AdicionarItemPedido(pedidoItem));
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente Acima do Permitido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistenteSomaUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedidoItem = new PedidoItem(_produtoId, "Produto Teste", 1, 100);
            var pedidoItem2 = new PedidoItem(_produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM, 100);
            _pedido.AdicionarItemPedido(pedidoItem);

            // Act & Assert
            Assert.Throws<DomainException>(() => _pedido.AdicionarItemPedido(pedidoItem2));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _pedido.AtualizarItemPedido(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Válido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var pedidoItem = new PedidoItem(_produtoId, "Produto Teste", 2, 100);
            _pedido.AdicionarItemPedido(pedidoItem);
            var pedidoItemAtualizado = new PedidoItem(_produtoId, "Produto Teste", 5, 100);
            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            // Act
            _pedido.AtualizarItemPedido(pedidoItemAtualizado);

            // Assert
            Assert.Equal(novaQuantidade, _pedido.PedidoItens.FirstOrDefault(p => p.ProdutoId == _produtoId).Quantidade);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Validar Total")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedidoItemExistente1 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);
            var pedidoItemExistente2 = new PedidoItem(_produtoId, "Produto Teste 2", 3, 15);
            _pedido.AdicionarItemPedido(pedidoItemExistente1);
            _pedido.AdicionarItemPedido(pedidoItemExistente2);

            var pedidoItemAtualizado = new PedidoItem(_produtoId, "Produto Teste 2", 5, 15);
            var totalPedido = pedidoItemExistente1.Quantidade * pedidoItemExistente1.ValorUnitario + pedidoItemAtualizado.Quantidade * pedidoItemAtualizado.ValorUnitario;

            // Act
            _pedido.AtualizarItemPedido(pedidoItemAtualizado);

            // Assert
            Assert.Equal(totalPedido, _pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade Acima do Permitido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedidoItemExistente1 = new PedidoItem(_produtoId, "Produto Teste", 3, 15);
            _pedido.AdicionarItemPedido(pedidoItemExistente1);

            var pedidoItemAtualizado = new PedidoItem(_produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 15);

            // Act & Assert
            Assert.Throws<DomainException>(() => _pedido.AtualizarItemPedido(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => _pedido.RemoverItemPedido(pedidoItemRemover));
        }

        [Fact(DisplayName = "Remover Item Pedido Deve Calcular Valor Total")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemExistente_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Teste 1", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste 2", 3, 15);
            _pedido.AdicionarItemPedido(pedidoItem1);
            _pedido.AdicionarItemPedido(pedidoItem2);

            var totalPedido = pedidoItem2.Quantidade * pedidoItem2.ValorUnitario;

            // Act
            _pedido.RemoverItemPedido(pedidoItem1);

            // Assert
            Assert.Equal(totalPedido, _pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Válido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherValido_DeveRetornarSemErros()
        {
            // Arrange
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = _pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar Voucher Inválido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherInvalido_DeveRetornarComErros()
        {
            // Arrange
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(-1), true, true);

            // Act
            var result = _pedido.AplicarVoucher(voucher);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar Voucher Tipo Valor Desconto")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoValorDesconto_DeveDescontarDoValorTotal()
        {
            // Arrange
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Teste 1", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste 2", 3, 15);
            _pedido.AdicionarItemPedido(pedidoItem1);
            _pedido.AdicionarItemPedido(pedidoItem2);

            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(10), true, false);
            var valorComDesconto = _pedido.ValorTotal - voucher.ValorDesconto;

            // Act
            _pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(valorComDesconto, _pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Tipo Percentual Desconto")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoPercentualDesconto_DeveDescontarDoValorTotal()
        {
            // Arrange
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Teste 1", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste 2", 3, 15);
            _pedido.AdicionarItemPedido(pedidoItem1);
            _pedido.AdicionarItemPedido(pedidoItem2);

            var voucher = new Voucher("PROMO-15-REAIS", 15, null, TipoDescontoVoucher.Porcentagem, 1, DateTime.Now.AddDays(10), true, false);
            var valorDesconto = (_pedido.ValorTotal * voucher.PercentualDesconto) / 100;
            var valorComDesconto = _pedido.ValorTotal - valorDesconto;

            // Act
            _pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(valorComDesconto, _pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Desconto Excede Valor Total Pedido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AplicarVoucher_DescontoExcedeValorTotalPedido_PedidoDeveTerValorZero()
        {
            // Arrange
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Teste 1", 2, 100);
            _pedido.AdicionarItemPedido(pedidoItem1);

            var voucher = new Voucher("PROMO-15-OFF", null, 300, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(10), true, false);

            // Act
            _pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(0, _pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Recalcular Desconto na Modificação do Pedido")]
        [Trait("02 - TDD", "Vendas - Pedido")]
        public void AplicarVoucher_ModificarItensPedido_DeveCalcularDescontoValorTotal()
        {
            // Arrange
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Teste 1", 2, 100);
            _pedido.AdicionarItemPedido(pedidoItem1);

            var voucher = new Voucher("PROMO-15-OFF", null, 50, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(10), true, false);
            _pedido.AplicarVoucher(voucher);

            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste 2", 4, 25);

            // Act
            _pedido.AdicionarItemPedido(pedidoItem2);

            // Assert
            var totalEsperado = _pedido.PedidoItens.Sum(i => i.Quantidade * i.ValorUnitario) - voucher.ValorDesconto;
            Assert.Equal(totalEsperado, _pedido.ValorTotal);
        }
    }
}
