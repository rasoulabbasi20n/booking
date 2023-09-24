using Framework.Domain;

namespace Framework.Application.Events
{
    public interface IDomainEventBus
    {
        Task<IApplicationEvent[]> Publish<T>(T @event, CancellationToken cancellationToken = default) where T : IDomainEvent;
    }
}
