using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.Core.Logging;
using LSS.UI.Diagnostics;
using LSS.Word.Diagnostics;

namespace LSS.WordAddIn.Commands;

/// <summary>
/// Opens the developer diagnostics window.
/// </summary>
[Command(CommandIds.Diagnostics, "Diagnostics", "Developer", Description = "Shows the LSS runtime diagnostics window.", DeveloperOnly = true)]
public sealed class DiagnosticsCommand : CommandBase
{
    private readonly IDiagnosticsService _diagnosticsService;
    private readonly IWordDiagnosticsService _wordDiagnosticsService;

    public DiagnosticsCommand(
        IDiagnosticsService diagnosticsService,
        IWordDiagnosticsService wordDiagnosticsService,
        IAppLogger logger)
        : base("Diagnostics", logger)
    {
        _diagnosticsService = diagnosticsService;
        _wordDiagnosticsService = wordDiagnosticsService;
    }

    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var sections = new List<DiagnosticsSection>(_diagnosticsService.GetSnapshot())
        {
            new DiagnosticsSection("Word Application", _wordDiagnosticsService.GetApplicationLines()),
            new DiagnosticsSection("Word Document", _wordDiagnosticsService.GetDocumentLines()),
            new DiagnosticsSection("Word Selection", _wordDiagnosticsService.GetSelectionLines())
        };

        using var form = new DiagnosticsForm(sections);
        form.ShowDialog();
        return Task.CompletedTask;
    }
}
