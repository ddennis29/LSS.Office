using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.Core.Logging;
using LSS.UI.Diagnostics;
using LSS.Word.Interop;

namespace LSS.WordAddIn.Commands;

/// <summary>
/// Opens the developer diagnostics window.
/// </summary>
public sealed class DiagnosticsCommand : CommandBase
{
    private readonly IDiagnosticsService _diagnosticsService;
    private readonly IWordDocumentService _wordDocumentService;

    public DiagnosticsCommand(
        IDiagnosticsService diagnosticsService,
        IWordDocumentService wordDocumentService,
        IAppLogger logger)
        : base("Diagnostics", logger)
    {
        _diagnosticsService = diagnosticsService;
        _wordDocumentService = wordDocumentService;
    }

    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var sections = new System.Collections.Generic.List<LSS.Core.Diagnostics.DiagnosticsSection>(_diagnosticsService.GetSnapshot())
        {
            new("Word", new[]
            {
                $"Document count: {_wordDocumentService.GetDocumentCount()}",
                $"Name: {_wordDocumentService.GetActiveDocumentName() ?? "No active document"}",
                $"Path: {_wordDocumentService.GetActiveDocumentPath() ?? "No active document"}",
                $"Paragraphs: {_wordDocumentService.GetParagraphCount()}",
                $"Tables: {_wordDocumentService.GetTableCount()}"
            })
        };

        using var form = new DiagnosticsForm(sections);
        form.ShowDialog();
        return Task.CompletedTask;
    }
}
