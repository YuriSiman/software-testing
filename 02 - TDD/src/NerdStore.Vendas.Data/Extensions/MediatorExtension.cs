using MediatR;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Data.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos(this IMediator mediator, VendasContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvents) =>
                {
                    await mediator.Publish(domainEvents);
                });

            await Task.WhenAll(tasks);
        }
    }
}
