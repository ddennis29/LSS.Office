using System;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Interop;

public sealed class WordDocumentService : IWordDocumentService
{
    private readonly Word.Application _application;

    public WordDocumentService(Word.Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public string? GetActiveDocumentName()
    {
        return _application.Documents.Count == 0 ? null : _application.ActiveDocument.Name;
    }

    public string? GetActiveDocumentPath()
    {
        return _application.Documents.Count == 0 ? null : _application.ActiveDocument.FullName;
    }

    public int GetParagraphCount()
    {
        return _application.Documents.Count == 0 ? 0 : _application.ActiveDocument.Paragraphs.Count;
    }
}
