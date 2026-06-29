using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Logging;
using LSS.Word.Interop;

namespace LSS.WordAddIn.Commands;

/// <summary>
/// Inserts a small verification message into the active Word selection.
/// </summary>
public sealed class InsertDiagnosticTextCommand : CommandBase
{
    private readonly IWordSelectionService _selectionService;

    public InsertDiagnosticTextCommand(IWordSelectionService selectionService, IAppLogger logger)
        : base("Insert Diagnostic Text", logger)
    {
        _selectionService = selectionService;
    }

    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        _selectionService.InsertText("LSS Office Suite diagnostics insert verified.");
        return Task.CompletedTask;
    }
}
