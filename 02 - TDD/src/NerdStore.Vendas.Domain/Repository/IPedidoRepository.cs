using NerdStore.Core.Data;

namespace NerdStore.Vendas.Domain.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);
    }
}
