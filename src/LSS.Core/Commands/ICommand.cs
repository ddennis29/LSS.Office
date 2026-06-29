using System.Threading;
using System.Threading.Tasks;

namespace LSS.Core.Commands;

/// <summary>
/// Represents executable application behavior triggered by the Ribbon, task panes, or developer tools.
/// </summary>
public interface ICommand
{
    string Name { get; }
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
