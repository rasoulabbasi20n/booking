using Autofac;
using Framework.Application.Commands;
using Framework.Application.Events;
using Framework.Domain;
using Framework.Problems;

namespace Framework.Autofac
{
    public class AutofacCommandBus : ICommandBus
    {
        private readonly ILifetimeScope _scope;
        private readonly ILoggingService _logger;
        private readonly IDomainEventBus _domainEventBus;

        public AutofacCommandBus(ILifetimeScope scope, ILoggingService logger, IDomainEventBus domainEventBus)
        {
            _scope = scope;
            _logger = logger;
            _domainEventBus = domainEventBus;
        }

        public async Task<ClientResponse> Send<TCommand>(TCommand command, CommandOptions commandOptions = default, CancellationToken token = default)
        {
            try
            {
                var handler = _scope.Resolve<ICommandHandler<TCommand>>();
                var commandResult = await handler.Execute(command, token);

                if (commandResult.Success)
                {
                    await ProcessEvents(commandResult, token);
                    return ClientResponse.CreateSuccess();
                }

                HandleProblem(commandResult.Problem);
                return ClientResponse.CreateFailure(commandResult.Problem);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<ClientResponse<TResult>> SendAndReply<TCommand, TResult>(TCommand command, CommandOptions commandOptions = default, CancellationToken token = default)
        {
            try
            {
                var handler = _scope.Resolve<ICommandHandler<TCommand, TResult>>();
                var commandResult = await handler.Execute(command, token);

                if (commandResult.Success)
                {
                    await ProcessEvents(commandResult, token);
                    return ClientResponse<TResult>.CreateSuccess(commandResult.Result!);
                }

                HandleProblem(commandResult!.Problem!);
                return ClientResponse<TResult>.CreateFailure(commandResult.Problem!);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private async Task ProcessEvents(CommandResult result, CancellationToken token)
        {
            var appEvents = new List<IApplicationEvent>(result.RaisedApplicationEvents);
            appEvents.AddRange(await ProcessDomainEvents(result.RaisedDomainEvents, token));
            await SendApplicationEvents(appEvents, token);
        }

        private async Task<List<IApplicationEvent>> ProcessDomainEvents(IDomainEvent[] domainEvents, CancellationToken cancellationToken)
        {
            var appEvents = new List<IApplicationEvent>();
            foreach (var @event in domainEvents)
                appEvents.AddRange(await _domainEventBus.Publish(@event, cancellationToken));

            return appEvents;
        }

        private async Task SendApplicationEvents(IEnumerable<IApplicationEvent> applicationEvents, CancellationToken cancellationToken)
        {
            // Method intentionally left empty.
        }

        private void HandleProblem(ProblemBase problem)
        {
            _logger.Error(problem);
        }
    }
}