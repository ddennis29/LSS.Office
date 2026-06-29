using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Interop;

public sealed class WordApplicationService : IWordApplicationService
{
    private readonly Word.Application _application;

    public WordApplicationService(Word.Application application)
    {
        _application = application;
    }

    public string GetActiveDocumentName()
    {
        try { return _application.ActiveDocument?.Name ?? string.Empty; }
        catch { return string.Empty; }
    }

    public string GetSelectionText()
    {
        try { return _application.Selection?.Text ?? string.Empty; }
        catch { return string.Empty; }
    }

    public void InsertTextAtSelection(string text)
    {
        _application.Selection.TypeText(text);
    }
}
