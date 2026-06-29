using System.Threading;
using System.Threading.Tasks;

namespace LSS.Core.Commands;

/// <summary>
/// Executes commands through the dependency injection container.
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// Executes a command by its concrete type.
    /// </summary>
    Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = default) where TCommand : ICommand;

    /// <summary>
    /// Executes a command by a stable command identifier.
    /// </summary>
    Task ExecuteAsync(string commandId, CancellationToken cancellationToken = default);
}
