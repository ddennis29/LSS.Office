using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Word.Interop;

namespace LSS.WordAddIn.Commands;

public sealed class InsertDiagnosticTextCommand : ICommand
{
    private readonly IWordSelectionService _selectionService;

    public InsertDiagnosticTextCommand(IWordSelectionService selectionService)
    {
        _selectionService = selectionService;
    }

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        _selectionService.ReplaceSelectedText("LSS Office Suite diagnostics insert verified.");
        return Task.CompletedTask;
    }
}
