using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.Core.Logging;
using LSS.UI.Diagnostics;

namespace LSS.WordAddIn.Commands;

/// <summary>
/// Shows every command registered with the runtime.
/// </summary>
[Command(CommandIds.CommandBrowser, "Command Browser", "Developer", Description = "Displays registered command metadata.", DeveloperOnly = true)]
public sealed class ShowCommandBrowserCommand : CommandBase
{
    private readonly ICommandRegistry _commandRegistry;

    public ShowCommandBrowserCommand(ICommandRegistry commandRegistry, IAppLogger logger)
        : base("Command Browser", logger)
    {
        _commandRegistry = commandRegistry;
    }

    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var lines = _commandRegistry.Descriptors
            .Select(x => $"{x.Id}\t{x.DisplayName}\t{x.Category}\tDeveloperOnly={x.DeveloperOnly}\t{x.ImplementationType.FullName}\t{x.Description}")
            .DefaultIfEmpty("No commands registered.");

        using var form = new DiagnosticsForm(new[] { new DiagnosticsSection("Commands", lines) });
        form.ShowDialog();
        return Task.CompletedTask;
    }
}
