using LSS.Word.Models;

namespace LSS.Word.Interop;

/// <summary>
/// Provides safe access to the active Word document.
/// </summary>
public interface IWordDocumentService
{
    int GetDocumentCount();
    string? GetActiveDocumentName();
    string? GetActiveDocumentPath();
    int GetParagraphCount();
    int GetTableCount();
    int GetWordCount();
    bool HasActiveDocument();
    WordDocumentSnapshot GetSnapshot();
}
