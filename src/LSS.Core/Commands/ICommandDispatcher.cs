using System.Threading;
using System.Threading.Tasks;

namespace LSS.Core.Commands;

public interface ICommandDispatcher
{
    Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = default)
        where TCommand : ICommand;
}
