using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.UI.Diagnostics;
using LSS.Word.Interop;

namespace LSS.WordAddIn.Commands;

public sealed class DiagnosticsCommand : ICommand
{
    private readonly IDiagnosticsService _diagnosticsService;
    private readonly IWordDocumentService _wordDocumentService;

    public DiagnosticsCommand(IDiagnosticsService diagnosticsService, IWordDocumentService wordDocumentService)
    {
        _diagnosticsService = diagnosticsService;
        _wordDocumentService = wordDocumentService;
    }

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var lines = new System.Collections.Generic.List<string>(_diagnosticsService.GetStartupReport())
        {
            string.Empty,
            "Word Document",
            "-------------",
            $"Name: {_wordDocumentService.GetActiveDocumentName() ?? "No active document"}",
            $"Path: {_wordDocumentService.GetActiveDocumentPath() ?? "No active document"}",
            $"Paragraphs: {_wordDocumentService.GetParagraphCount()}"
        };

        using var form = new DiagnosticsForm(lines);
        form.ShowDialog();
        return Task.CompletedTask;
    }
}
