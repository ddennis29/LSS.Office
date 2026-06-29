using System;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Interop;

/// <summary>
/// Word selection wrapper. Office COM access should stay inside this layer.
/// </summary>
public sealed class WordSelectionService : IWordSelectionService
{
    private readonly Word.Application _application;

    public WordSelectionService(Word.Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public bool HasSelection() => _application.Selection is not null;

    public string GetSelectedText() => _application.Selection?.Text ?? string.Empty;

    public void ReplaceSelectedText(string text)
    {
        if (_application.Selection is null) return;
        _application.Selection.Text = text ?? string.Empty;
    }

    public void InsertText(string text)
    {
        if (_application.Selection is null) return;
        _application.Selection.TypeText(text ?? string.Empty);
    }
}
