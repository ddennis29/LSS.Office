using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Logging;
using LSS.UI.Dialogs;
using LSS.Word.Interop;

namespace LSS.WordAddIn.Commands;

public sealed class DiagnosticsCommand : CommandBase
{
    private readonly IWordApplicationService _word;
    private readonly IMessageDialogService _dialogs;

    public DiagnosticsCommand(IAppLogger logger, IWordApplicationService word, IMessageDialogService dialogs)
        : base("Diagnostics", logger)
    {
        _word = word;
        _dialogs = dialogs;
    }

    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var docName = _word.GetActiveDocumentName();
        _dialogs.ShowInformation("LSS Office Suite", string.IsNullOrWhiteSpace(docName)
            ? "LSS Office Suite is loaded. No active document was detected."
            : $"LSS Office Suite is loaded. Active document: {docName}");
        return Task.CompletedTask;
    }
}
