using System;
using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LSS.Core.Commands;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _services;
    private readonly IAppLogger _logger;

    public CommandDispatcher(IServiceProvider services, IAppLogger logger)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var commandName = typeof(TCommand).Name;
        try
        {
            _logger.Information($"Executing command: {commandName}");
            var command = _services.GetRequiredService<TCommand>();
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
