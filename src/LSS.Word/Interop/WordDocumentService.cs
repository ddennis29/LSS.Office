using System;
using LSS.Word.Models;
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

    public int GetWordCount() => HasActiveDocument() ? _application.ActiveDocument.Words.Count : 0;

    public WordDocumentSnapshot GetSnapshot()
    {
        if (!HasActiveDocument())
        {
            return new WordDocumentSnapshot(false, string.Empty, string.Empty, 0, 0, 0, true);
        }

        var document = _application.ActiveDocument;
        return new WordDocumentSnapshot(
            true,
            document.Name ?? string.Empty,
            document.FullName ?? string.Empty,
            document.Paragraphs.Count,
            document.Tables.Count,
            document.Words.Count,
            document.Saved);
    }
}
