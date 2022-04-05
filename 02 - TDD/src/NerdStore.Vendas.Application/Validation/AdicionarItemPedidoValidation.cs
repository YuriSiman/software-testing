using FluentValidation;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Validation
{
    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id do cliente inválido";
        public static string IdProdutoErroMsg => "Id do produto inválido";
        public static string NomeErroMsg => "O nome do produto não foi informado";
        public static string QtdMaxErroMsg => $"A quantidade máxima de um item é {Pedido.MAX_UNIDADES_ITEM}";
        public static string QtdMinErroMsg => $"A quantidade mínima de um item é {Pedido.MIN_UNIDADES_ITEM}";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        public AdicionarItemPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QtdMinErroMsg)
                .LessThanOrEqualTo(Pedido.MAX_UNIDADES_ITEM)
                .WithMessage(QtdMaxErroMsg);

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
