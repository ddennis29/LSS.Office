namespace LSS.Word.Interop;

public interface IWordDocumentService
{
    string? GetActiveDocumentName();
    string? GetActiveDocumentPath();
    int GetParagraphCount();
}
