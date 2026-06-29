using System;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Interop;

public sealed class WordSelectionService : IWordSelectionService
{
    private readonly Word.Application _application;

    public WordSelectionService(Word.Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public string GetSelectedText()
    {
        return _application.Selection?.Text ?? string.Empty;
    }

    public void ReplaceSelectedText(string text)
    {
        if (_application.Selection is null) return;
        _application.Selection.Text = text ?? string.Empty;
    }
}
