﻿using Autofac;
using Framework.Application;
using Framework.Application.Commands;
using Framework.Application.Events;

namespace Framework.Autofac
{
    public class AutofacCommandBus : CommandBusBase
    {
        private readonly ILifetimeScope _scope;

        public AutofacCommandBus(ILifetimeScope scope, ILoggingService logger, IDomainEventBus domainEventBus, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork, domainEventBus)
        {
            _scope = scope;
        }

        protected override ICommandHandler<TCommand> ResolveHandler<TCommand>()
        {
            return _scope.Resolve<ICommandHandler<TCommand>>();
        }

        protected override ICommandHandler<TCommand, TResult> ResolveHandler<TCommand, TResult>()
        {
            return _scope.Resolve<ICommandHandler<TCommand, TResult>>();

        }
    }
}