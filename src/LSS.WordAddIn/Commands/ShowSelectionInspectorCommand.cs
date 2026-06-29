using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.Core.Logging;
using LSS.UI.Diagnostics;
using LSS.Word.Diagnostics;

namespace LSS.WordAddIn.Commands;

/// <summary>
/// Shows details about the active Word selection.
/// </summary>
[Command(CommandIds.SelectionInspector, "Selection Inspector", "Developer", Description = "Shows current selection diagnostics.", DeveloperOnly = true)]
public sealed class ShowSelectionInspectorCommand : CommandBase
{
    private readonly IWordDiagnosticsService _wordDiagnosticsService;

    public ShowSelectionInspectorCommand(IWordDiagnosticsService wordDiagnosticsService, IAppLogger logger)
        : base("Selection Inspector", logger)
    {
        _wordDiagnosticsService = wordDiagnosticsService;
    }

    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        using var form = new DiagnosticsForm(new[]
        {
            new DiagnosticsSection("Selection", _wordDiagnosticsService.GetSelectionLines()),
            new DiagnosticsSection("Document", _wordDiagnosticsService.GetDocumentLines())
        });
        form.ShowDialog();
        return Task.CompletedTask;
    }
}
