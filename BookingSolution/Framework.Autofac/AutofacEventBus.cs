using Autofac;
using Framework.Application.Events;
using Framework.Domain;

namespace Framework.Autofac
{
    public class AutofacEventBus : IDomainEventBus
    {
        private readonly ILifetimeScope _scope;
        private readonly ILoggingService _logger;

        public AutofacEventBus(ILifetimeScope scope, ILoggingService logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public async Task<IApplicationEvent[]> Publish<T>(T @event, CancellationToken cancellationToken = default) where T : IDomainEvent
        {
            try
            {
                var handlers = _scope.Resolve<IEnumerable<IDomainEventHandler<T>>>();
                var appEvents = new List<IApplicationEvent>();
                foreach (var handler in handlers)
                    appEvents.AddRange(await handler.Handle(@event, cancellationToken));

                return appEvents.ToArray();
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.Error(ex);
                }
                catch
                {
                    // ignored
                }

                throw;
            }
        }
    }
}
