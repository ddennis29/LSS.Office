using System;
using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LSS.Core.Commands;

/// <summary>
/// Default command dispatcher with centralized logging and exception handling.
/// </summary>
public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _services;
    private readonly ICommandRegistry _registry;
    private readonly IAppLogger _logger;

    public CommandDispatcher(IServiceProvider services, ICommandRegistry registry, IAppLogger logger)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        await ExecuteCommandTypeAsync(typeof(TCommand), cancellationToken).ConfigureAwait(false);
    }

    public async Task ExecuteAsync(string commandId, CancellationToken cancellationToken = default)
    {
        if (!_registry.TryResolve(commandId, out var commandType))
        {
            throw new InvalidOperationException($"No command is registered for '{commandId}'.");
        }

        await ExecuteCommandTypeAsync(commandType, cancellationToken).ConfigureAwait(false);
    }

    private async Task ExecuteCommandTypeAsync(Type commandType, CancellationToken cancellationToken)
    {
        var commandName = commandType.Name;
        try
        {
            _logger.Information($"Executing command: {commandName}");
            var command = (ICommand)_services.GetRequiredService(commandType);
            await command.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            _logger.Information($"Command completed: {commandName}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Command failed: {commandName}");
            throw;
        }
    }
}
