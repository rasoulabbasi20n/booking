using Framework.Domain;

namespace Framework.Application.Events
{
    public abstract class DomainEventBusBase : IDomainEventBus
    {
        private readonly ILoggingService _loggerService;

        protected DomainEventBusBase(ILoggingService logger)
        {
            _loggerService = logger;
        }

        public async Task<IApplicationEvent[]> Publish<T>(T @event, CancellationToken cancellationToken = default) where T : IDomainEvent
        {
            try
            {
                var handlers = ResolveHandlers<T>();
                var appEvents = new List<IApplicationEvent>();
                foreach (var handler in handlers)
                    appEvents.AddRange(await handler.Handle(@event, cancellationToken));

                return appEvents.ToArray();
            }
            catch (Exception ex)
            {
                try
                {
                    _loggerService.Error(ex);
                }
                catch
                {
                    // ignored
                }

                throw;
            }
        }

        protected abstract IEnumerable<IDomainEventHandler<T>> ResolveHandlers<T>() where T : IDomainEvent;
    }
}
