using Autofac;
using Framework.Application.Events;
using Framework.Domain;

namespace Framework.Autofac
{
    public class AutofacEventBus : DomainEventBusBase
    {
        private readonly ILifetimeScope _scope;

        public AutofacEventBus(ILifetimeScope scope, ILoggingService logger) : base(logger)
        {
            _scope = scope;
        }

        protected override IEnumerable<IDomainEventHandler<T>> ResolveHandlers<T>()
        {
            return _scope.Resolve<IEnumerable<IDomainEventHandler<T>>>();
        }
    }
}
