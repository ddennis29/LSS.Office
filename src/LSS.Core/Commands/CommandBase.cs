using System;
using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Logging;

namespace LSS.Core.Commands;

/// <summary>
/// Base command implementation that provides consistent logging and error handling.
/// </summary>
public abstract class CommandBase : ICommand
{
    private readonly IAppLogger _logger;

    protected CommandBase(string name, IAppLogger logger)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string Name { get; }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.Information("Executing command {CommandName}", Name);
            await ExecuteCoreAsync(cancellationToken).ConfigureAwait(false);
            _logger.Information("Completed command {CommandName}", Name);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Command failed: {CommandName}", Name);
            throw;
        }
    }

    protected abstract Task ExecuteCoreAsync(CancellationToken cancellationToken);
}
