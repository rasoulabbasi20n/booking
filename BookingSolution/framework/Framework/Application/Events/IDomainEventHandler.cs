using Framework.Domain;

namespace Framework.Application.Events
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task<IApplicationEvent[]> Handle(TEvent @event, CancellationToken cancellationToken = default);
    }
}
