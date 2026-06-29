using System;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Interop;

/// <summary>
/// Word document wrapper. Office COM access should stay inside this layer.
/// </summary>
public sealed class WordDocumentService : IWordDocumentService
{
    private readonly Word.Application _application;

    public WordDocumentService(Word.Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public int GetDocumentCount() => _application.Documents.Count;

    public bool HasActiveDocument() => _application.Documents.Count > 0;

    public string? GetActiveDocumentName() => HasActiveDocument() ? _application.ActiveDocument.Name : null;

    public string? GetActiveDocumentPath() => HasActiveDocument() ? _application.ActiveDocument.FullName : null;

    public int GetParagraphCount() => HasActiveDocument() ? _application.ActiveDocument.Paragraphs.Count : 0;

    public int GetTableCount() => HasActiveDocument() ? _application.ActiveDocument.Tables.Count : 0;
}
